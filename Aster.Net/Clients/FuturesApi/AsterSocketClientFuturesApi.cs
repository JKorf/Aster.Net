using Aster.Net.Clients.MessageHandlers;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesApi
{
    /// <summary>
    /// Client providing access to the Aster Futures websocket Api
    /// </summary>
    internal partial class AsterSocketClientFuturesApi : SocketApiClient, IAsterSocketClientFuturesApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _streamPath = MessagePath.Get().Property("stream");
        private static readonly MessagePath _ePath = MessagePath.Get().Property("data").Property("e");

        private readonly HashSet<string> _userEvents = new HashSet<string>
        {
            "ACCOUNT_CONFIG_UPDATE",
            "MARGIN_CALL",
            "ACCOUNT_UPDATE",
            "ORDER_TRADE_UPDATE",
            "listenKeyExpired",
        };

        protected override ErrorMapping ErrorMapping => AsterErrors.FuturesErrors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal AsterSocketClientFuturesApi(ILogger logger, AsterSocketOptions options) :
            base(logger, options.Environment.FuturesSocketClientAddress!, options, options.FuturesOptions)
        {
            RateLimiter = AsterExchange.RateLimiter.Socket;
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(AsterExchange._serializerContext);
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(AsterExchange._serializerContext);

        /// <inheritdoc />
        public override ISocketMessageHandler CreateMessageConverter(WebSocketMessageType messageType)
            => new AsterSocketFuturesMessageConverter();

        /// <inheritdoc />
        protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
            => new AsterAuthenticationProvider(credentials);


        #region Aggregate Trade Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default) 
            => await SubscribeToAggregatedTradeUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default)
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

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToAggregatedTradeUpdatesPerfAsync(IEnumerable<string> symbols, Action<AsterAggregatedTradeUpdate> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@aggTrade"));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterAggregatedTradeUpdate>, AsterAggregatedTradeUpdate>(BaseAddress, topics.ToArray(), callback, ct: ct);
        }
        #endregion

        #region Mark Price Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(int? updateInterval, Action<DataEvent<AsterMarkPriceUpdate[]>> onMessage, CancellationToken ct = default)
        {
            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterMarkPriceUpdate[]>>((receiveTime, originalData, data) =>
            {
                onMessage(
                    new DataEvent<AsterMarkPriceUpdate[]>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithDataTimestamp(data.Data.Max(x => x.EventTime), GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            return await SubscribeAsync(BaseAddress, "markPriceUpdate",["!markPrice@arr" + (updateInterval == 1000 ? "@1s" : "")], handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<AsterMarkPriceUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToMarkPriceUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<AsterMarkPriceUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            updateInterval?.ValidateIntValues(nameof(updateInterval), 1000, 3000);

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterMarkPriceUpdate>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterMarkPriceUpdate>(Exchange, data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@markPrice" + (updateInterval == 1000 ? "@1s" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, "markPriceUpdate", symbols, handler, ct).ConfigureAwait(false);
        }
        #endregion

        #region Kline/Candlestick Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, interval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToKlineUpdatesAsync(new[] { symbol }, intervals, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default) =>
            await SubscribeToKlineUpdatesAsync(symbols, new[] { interval }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToMiniTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<AsterMiniTickUpdate[]>> onMessage, CancellationToken ct = default)
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

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToMiniTickerUpdatesPerfAsync(IEnumerable<string> symbols, Action<AsterMiniTickUpdate> onMessage, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@miniTicker"));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterMiniTickUpdate>, AsterMiniTickUpdate>(BaseAddress, topics.ToArray(), onMessage, ct: ct);
        }
        #endregion

        #region Symbol Ticker Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<AsterTickerUpdate[]>> onMessage, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default) 
            => await SubscribeToBookTickerUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default)
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
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default)
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

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToBookTickerUpdatesPerfAsync(IEnumerable<string> symbols, Action<AsterBookTickerUpdate> callback, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(x => x.ToLowerInvariant() + "@bookTicker"));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterBookTickerUpdate>, AsterBookTickerUpdate>(BaseAddress, topics.ToArray(), callback, ct);
        }

        #endregion

        #region Liquidation Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<AsterLiquidationUpdate>> onMessage, CancellationToken ct = default)
            => await SubscribeToLiquidationUpdatesAsync(new[] { symbol }, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterLiquidationUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));

            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterLiquidationUpdateEvent>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterLiquidationUpdate>(Exchange, data.Data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@forceOrder").ToArray();
            return await SubscribeAsync(BaseAddress, "forceOrder", symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(Action<DataEvent<AsterLiquidationUpdate>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DateTime, string?, AsterCombinedStream<AsterLiquidationUpdateEvent>>((receiveTime, originalData, data) =>
            {
                UpdateTimeOffset(data.Data.EventTime);

                onMessage(
                    new DataEvent<AsterLiquidationUpdate>(Exchange, data.Data.Data, receiveTime, originalData)
                        .WithStreamId(data.Stream)
                        .WithSymbol(data.Data.Data.Symbol)
                        .WithDataTimestamp(data.Data.EventTime, GetTimeOffset())
                        .WithUpdateType(SocketUpdateType.Update)
                    );
            });
            return await SubscribeAsync(BaseAddress, "forceOrder", new[] { "!forceOrder@arr" }, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region Partial Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default) 
            => await SubscribeToPartialOrderBookUpdatesAsync(new[] { symbol }, levels, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            levels.ValidateIntValues(nameof(levels), 5, 10, 20);
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 250, 500);

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

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToPartialOrderBookUpdatesPerfAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<AsterOrderBookUpdate> onMessage, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterOrderBookUpdate>, AsterOrderBookUpdate>(BaseAddress, topics.ToArray(), onMessage, ct: ct);
        }
        #endregion

        #region Diff. Book Depth Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default) 
            => await SubscribeToOrderBookUpdatesAsync(new[] { symbol }, updateInterval, onMessage, ct).ConfigureAwait(false);

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default)
        {
            symbols.ValidateNotNull(nameof(symbols));
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 250, 500);

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

        public Task<CallResult<HighPerfUpdateSubscription>> SubscribeToOrderBookUpdatesPerfAsync(IEnumerable<string> symbols, int? updateInterval, Action<AsterOrderBookUpdate> onMessage, CancellationToken ct)
        {
            var topics = new HashSet<string>(symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")));
            return SubscribeHighPerfAsync<AsterCombinedStream<AsterOrderBookUpdate>, AsterOrderBookUpdate>(BaseAddress, topics.ToArray(), onMessage, ct: ct);
        }

        #endregion

        #region User Data Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<AsterConfigUpdate>>? onConfigUpdate = null,
            Action<DataEvent<AsterMarginUpdate>>? onMarginUpdate = null,
            Action<DataEvent<AsterAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<AsterOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<AsterSocketEvent>>? onListenKeyExpired = null,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new AsterUserDataSubscription(_logger, this, listenKey, onOrderUpdate, onConfigUpdate, onMarginUpdate, onAccountUpdate, onListenKeyExpired);
            return await SubscribeInternalAsync(BaseAddress, subscription, ct).ConfigureAwait(false);
        }

        #endregion

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, string dataType, IEnumerable<string> topics, Action<DateTime, string?, T> onData, CancellationToken ct)
        {
            var subscription = new AsterSubscription<T>(_logger, dataType, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }
        internal Task<CallResult<HighPerfUpdateSubscription>> SubscribeHighPerfAsync<T, U>(
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

        internal Task<CallResult<UpdateSubscription>> SubscribeInternalAsync(string url, Subscription subscription, CancellationToken ct)
        {
            return base.SubscribeAsync(url.AppendPath("stream"), subscription, ct);
        }

        /// <inheritdoc />
        public override string? GetListenerIdentifier(IMessageAccessor message)
        {
            var id = message.GetValue<int?>(_idPath);
            if (id != null)
                return id.ToString();

            var stream = message.GetValue<string>(_streamPath);
            var e = message.GetValue<string>(_ePath);
            if (e != null && _userEvents.Contains(e))
                return stream + e;

            return stream;
        }

        /// <inheritdoc />
        public IAsterSocketClientFuturesApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => AsterExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
