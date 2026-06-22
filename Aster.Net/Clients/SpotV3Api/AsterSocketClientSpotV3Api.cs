using Aster.Net.Clients.MessageHandlers;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using Aster.Net.Objects.Options;
using Aster.Net.Objects.Sockets;
using Aster.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.HighPerf;
using CryptoExchange.Net.TokenManagement;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotV3Api
{
    /// <summary>
    /// Client providing access to the Aster Spot websocket Api
    /// </summary>
    internal partial class AsterSocketClientSpotV3Api : SocketApiClient<AsterEnvironment, AsterV1AuthenticationProvider, AsterCredentials>, IAsterSocketClientSpotV3Api
    {
        #region fields
        protected override ErrorMapping ErrorMapping => AsterErrors.SpotErrors;
        private readonly ILoggerFactory? _loggerFactory;
        private AsterRestClient? _tokenClient;
        internal TokenManager TokenManager { get; }
        private AsterRestClient TokenClient
        {
            get
            {
                if (_tokenClient == null)
                {
                    _tokenClient = new AsterRestClient(null, _loggerFactory, Options.Create(new AsterRestOptions
                    {
                        ApiCredentials = ApiCredentials,
                        Environment = ClientOptions.Environment,
                        Proxy = ClientOptions.Proxy,
                        OutputOriginalData = ClientOptions.OutputOriginalData
                    }));
                }

                return _tokenClient;
            }
        }
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal AsterSocketClientSpotV3Api(ILoggerFactory? loggerFactory, AsterSocketOptions options) :
            base(loggerFactory, AsterExchange.Metadata.Id, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
        {
            _loggerFactory = loggerFactory;

            RateLimiter = AsterExchange.RateLimiter.Socket;

            TokenManager = new TokenManager(
                AsterExchange.Metadata.Id,
                loggerFactory,
                TimeSpan.FromMinutes(30),
                TimeSpan.FromMinutes(60),
                startToken: StartListenKeyAsync,
                keepAliveToken: KeepAliveListenKeyAsync,
                stopToken: StopListenKeyAsync);
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(AsterExchange._serializerContext);

        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
            => new AsterSocketSpotMessageConverter();
        /// <inheritdoc />
        protected override AsterV1AuthenticationProvider CreateAuthenticationProvider(AsterCredentials credentials)
            => new AsterV1AuthenticationProvider(credentials);

        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterAggregatedTradeUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterAggregatedTradeUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@aggTrade").ToArray();
            return await SubscribeAsync(BaseAddress, "aggTrade", symbols, handler, ct).ConfigureAwait(false);
        }

        public Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeToAggregatedTradeUpdatesPerfAsync(IEnumerable<string> symbols, Action<AsterStreamMinimalTrade> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@aggTrade"));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterStreamMinimalTrade>, AsterStreamMinimalTrade>(BaseAddress, topics.ToArray(), callback, ct: ct);
        }
        #endregion

        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterKlineUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterKlineUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.SelectMany(a => intervals.Select(i =>
                a.ToLower(CultureInfo.InvariantCulture) + "@kline_" + EnumConverter.GetString(i))).ToArray();
            return await SubscribeAsync(BaseAddress, "kline", symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Symbol Mini Ticker Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterMiniTickUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterMiniTickUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@miniTicker").ToArray();
            return await SubscribeAsync(BaseAddress, "24hrMiniTicker", symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<AsterMiniTickUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterMiniTickUpdate[]>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<AsterMiniTickUpdate[]>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithDataTimestamp(data.Data.Max(x => x.EventTime), GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            return await SubscribeAsync(BaseAddress, "24hrMiniTicker", new[] { "!miniTicker@arr" }, handler, ct).ConfigureAwait(false);
        }

        public Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeToMiniTickerUpdatesPerfAsync(IEnumerable<string> symbols, Action<AsterMiniTickUpdate> onMessage, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@miniTicker"));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterMiniTickUpdate>, AsterMiniTickUpdate>(BaseAddress, topics.ToArray(), onMessage, ct: ct);
        }
        #endregion

        #region Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterTickerUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterTickerUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@ticker").ToArray();
            return await SubscribeAsync(BaseAddress, "24hrTicker", symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<AsterTickerUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterTickerUpdate[]>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<AsterTickerUpdate[]>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithDataTimestamp(data.Data.Max(x => x.EventTime), GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            return await SubscribeAsync(BaseAddress, "24hrTicker", new[] { "!ticker@arr" }, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Book Ticker Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterBookTickerUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterBookTickerUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@bookTicker").ToArray();
            return await SubscribeAsync(BaseAddress, "bookTicker", symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterBookTickerUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterBookTickerUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            return await SubscribeAsync(BaseAddress, "bookTicker", new[] { "!bookTicker" }, handler, ct).ConfigureAwait(false);
        }

        public Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeToBookTickerUpdatesPerfAsync(IEnumerable<string> symbols, Action<AsterBookTickerUpdate> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@bookTicker"));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterBookTickerUpdate>, AsterBookTickerUpdate>(BaseAddress, topics.ToArray(), callback, ct);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterOrderBookUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSequenceNumber(data.Data.LastUpdateId)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, "depthUpdate", symbols, handler, ct).ConfigureAwait(false);
        }

        public Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeToPartialOrderBookUpdatesPerfAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<AsterOrderBookUpdate> onMessage, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterOrderBookUpdate>, AsterOrderBookUpdate>(BaseAddress, topics.ToArray(), onMessage, ct: ct);
        }
        #endregion

        #region Diff. Book Depth Streams

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterOrderBookUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterOrderBookUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                        .WithSequenceNumber(data.Data.LastUpdateId)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, "depthUpdate", symbols, handler, ct).ConfigureAwait(false);
        }

        public Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeToOrderBookUpdatesPerfAsync(IEnumerable<string> symbols, int? updateInterval, Action<AsterOrderBookUpdate> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" +
                (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterOrderBookUpdate>, AsterOrderBookUpdate>(BaseAddress, topics.ToArray(), callback, ct: ct);
        }

        #endregion

        #region User Data Streams

        /// <inheritdoc />
        public Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            Action<DataEvent<AsterSpotAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<AsterSpotOrderUpdate>>? onOrderUpdate = null,
            CancellationToken ct = default)
            => SubscribeToUserDataUpdatesAsync(null, onAccountUpdate, onOrderUpdate, ct);

        /// <inheritdoc />
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string? listenKey,
            Action<DataEvent<AsterSpotAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<AsterSpotOrderUpdate>>? onOrderUpdate = null,
            CancellationToken ct = default)
        {
            if (listenKey == null && ApiCredentials!.V3 == null)
                return WebSocketResult.Fail<UpdateSubscription>(Exchange, new NoApiCredentialsError());

            TokenLease? lease = null;
            if (listenKey == null)
            {
                var leaseResult = await TokenManager.AcquireAsync(new TokenScope(
                    AsterExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.V3!.Key), ct).ConfigureAwait(false);
                if (!leaseResult.Success)
                    return WebSocketResult.Fail<UpdateSubscription>(Exchange, leaseResult.Error);

                lease = leaseResult.Data;
            }

            var subscription = new AsterSpotUserDataSubscription(_logger, this, listenKey, onOrderUpdate, onAccountUpdate)
            {
                TokenLease = lease
            };
            return await SubscribeInternalAsync(BaseAddress, subscription, ct).ConfigureAwait(false);
        }

        #endregion

        internal Task<WebSocketResult<UpdateSubscription>> SubscribeAsync<T>(string url, string dataType, IEnumerable<string> topics, Action<DateTime, string?, T> onData, CancellationToken ct)
        {
            var subscription = new AsterSubscription<T>(_logger, dataType, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        internal Task<WebSocketResult<HighPerfUpdateSubscription>> SubscribeHighPerfAsync<T, U>(
            string url,
            string[] topics,
            Action<U> onData,
            CancellationToken ct) where T : AsterCombinedStream<U>
        {
            var subscription = new AsterHighPerfSubscription<T>(topics, x =>
            {
                if (x.Data == null)
                {
                    // It's probably a different message (sub confirm for instance), ignore
                    return;
                }

                onData(x.Data);
            });

            return base.SubscribeHighPerfAsync(
                url.AppendPath("stream"),
                subscription,
                HighPerfConnectionFactory ??= new HighPerfJsonSocketConnectionFactory(AsterExchange._serializerContext),
                ct);
        }

        internal Task<WebSocketResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
        {
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        /// <inheritdoc />
        public IAsterSocketClientSpotV3ApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => AsterExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);


        protected override async Task<CallResult> RevitalizeRequestAsync(Subscription subscription)
        {
            if (subscription.TokenLease == null)
                return CallResult.Ok(); // Not an authenticated subscription, no need to revitalize

            var scope = new TokenScope(
                    AsterExchange.Metadata.Id,
                    EnvironmentName,
                    "Spot",
                    ApiCredentials!.V3!.Key);

            return await TokenManager.AcquireAndReplaceAsync(subscription, scope).ConfigureAwait(false);
        }

        private async Task<CallResult<string>> StartListenKeyAsync(TokenScope tokenScope, CancellationToken ct)
        {
            var result = await TokenClient.SpotV3Api.Account.StartUserStreamAsync(ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok(result.Data);
        }

        private async Task<CallResult> KeepAliveListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.SpotV3Api.Account.KeepAliveUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }

        private async Task<CallResult> StopListenKeyAsync(TokenInfo token, CancellationToken ct)
        {
            var result = await TokenClient.SpotV3Api.Account.StopUserStreamAsync(token.Token, ct).ConfigureAwait(false);
            if (!result.Success)
                return CallResult.Fail<string>(result.Error);

            return CallResult.Ok();
        }
    }
}
