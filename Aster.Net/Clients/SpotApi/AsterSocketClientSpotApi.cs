using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using Aster.Net.Objects.Options;
using Aster.Net.Objects.Sockets;
using Aster.Net.Objects.Sockets.Subscriptions;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotApi
{
    /// <summary>
    /// Client providing access to the Aster Spot websocket Api
    /// </summary>
    internal partial class AsterSocketClientSpotApi : SocketApiClient, IAsterSocketClientSpotApi
    {
        #region fields
        private static readonly MessagePath _idPath = MessagePath.Get().Property("id");
        private static readonly MessagePath _streamPath = MessagePath.Get().Property("stream");
        private static readonly MessagePath _ePath = MessagePath.Get().Property("data").Property("e");

        private readonly HashSet<string> _userEvents = new HashSet<string>
        {
            "outboundAccountPosition",
            "executionReport",
        };

        protected override ErrorMapping ErrorMapping => AsterErrors.SpotErrors;
        #endregion

        #region constructor/destructor

        /// <summary>
        /// ctor
        /// </summary>
        internal AsterSocketClientSpotApi(ILogger logger, AsterSocketOptions options) :
            base(logger, options.Environment.SpotSocketClientAddress!, options, options.SpotOptions)
        {
            RateLimiter = AsterExchange.RateLimiter.Socket;
        }
        #endregion

        /// <inheritdoc />
        protected override IByteMessageAccessor CreateAccessor(WebSocketMessageType type) => new SystemTextJsonByteMessageAccessor(AsterExchange._serializerContext);
        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(AsterExchange._serializerContext);

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

            var handler = new Action<DataEvent<AsterCombinedStream<AsterAggregatedTradeUpdate>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@aggTrade").ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<AsterCombinedStream<AsterKlineUpdate>>>(data =>
                onMessage(data.As<AsterKlineUpdate>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.SelectMany(a => intervals.Select(i =>
                a.ToLower(CultureInfo.InvariantCulture) + "@kline_" + EnumConverter.GetString(i))).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<AsterCombinedStream<AsterMiniTickUpdate>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@miniTicker").ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<AsterMiniTickUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<AsterCombinedStream<AsterMiniTickUpdate[]>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await SubscribeAsync(BaseAddress, new[] { "!miniTicker@arr" }, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<AsterCombinedStream<AsterTickerUpdate>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@ticker").ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<AsterTickerUpdate[]>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<AsterCombinedStream<AsterTickerUpdate[]>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithDataTimestamp(data.Data.Data.Max(x => x.EventTime))));
            return await SubscribeAsync(BaseAddress, new[] { "!ticker@arr" }, handler, ct).ConfigureAwait(false);
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

            var handler = new Action<DataEvent<AsterCombinedStream<AsterBookTickerUpdate>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@bookTicker").ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default)
        {
            var handler = new Action<DataEvent<AsterCombinedStream<AsterBookTickerUpdate>>>(data =>
                onMessage(data.As(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            return await SubscribeAsync(BaseAddress, new[] { "!bookTicker" }, handler, ct).ConfigureAwait(false);
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
            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);

            var handler = new Action<DataEvent<AsterCombinedStream<AsterOrderBookUpdate>>>(data =>
            {
                onMessage(data.As<AsterOrderBookUpdate>(data.Data.Data)
                    .WithStreamId(data.Data.Stream)
                    .WithSymbol(data.Data.Data.Symbol)
                    .WithDataTimestamp(data.Data.Data.EventTime));
            });

            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + levels + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
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

            updateInterval?.ValidateIntValues(nameof(updateInterval), 100, 1000);
            var handler = new Action<DataEvent<AsterCombinedStream<AsterOrderBookUpdate>>>(data =>
                onMessage(data.As<AsterOrderBookUpdate>(data.Data.Data)
                .WithStreamId(data.Data.Stream)
                .WithSymbol(data.Data.Data.Symbol)
                .WithDataTimestamp(data.Data.Data.EventTime)));
            symbols = symbols.Select(a => a.ToLower(CultureInfo.InvariantCulture) + "@depth" + (updateInterval.HasValue ? $"@{updateInterval.Value}ms" : "")).ToArray();
            return await SubscribeAsync(BaseAddress, symbols, handler, ct).ConfigureAwait(false);
        }

        #endregion

        #region User Data Streams

        /// <inheritdoc />
        public async Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<AsterSpotAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<AsterSpotOrderUpdate>>? onOrderUpdate = null,
            CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var subscription = new AsterSpotUserDataSubscription(_logger, listenKey, onOrderUpdate, onAccountUpdate);
            return await SubscribeInternalAsync(BaseAddress, subscription, ct).ConfigureAwait(false);
        }

        #endregion

        internal Task<CallResult<UpdateSubscription>> SubscribeAsync<T>(string url, IEnumerable<string> topics, Action<DataEvent<T>> onData, CancellationToken ct)
        {
            var subscription = new AsterSubscription<T>(_logger, topics.ToList(), onData, false);
            return SubscribeAsync(url.AppendPath("stream"), subscription, ct);
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
        protected override Task<Query?> GetAuthenticationRequestAsync(SocketConnection connection) => Task.FromResult<Query?>(null);

        /// <inheritdoc />
        public IAsterSocketClientSpotApiShared SharedClient => this;

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null)
            => AsterExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);
    }
}
