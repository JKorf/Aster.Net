using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using Aster.Net.Objects.Models;
using System.Collections.Generic;
using Aster.Net.Enums;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Aster Futures streams
    /// </summary>
    public interface IAsterSocketClientFuturesApi : ISocketApiClient, IDisposable
    {
        /// <summary>
        /// Subscribe to aggregated trade updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#aggregate-trade-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to aggregated trade updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#aggregate-trade-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#mark-price-stream" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="updateInterval">The interval for updates in milliseconds, either 1000 or 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<AsterMarkPriceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#mark-price-stream" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="updateInterval">The interval for updates in milliseconds, either 1000 or 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<AsterMarkPriceUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mark price updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#mark-price-stream-for-all-market" /></para>
        /// </summary>
        /// <param name="updateInterval">The interval for updates in milliseconds, either 1000 or 3000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceUpdatesAsync(int? updateInterval, Action<DataEvent<AsterMarkPriceUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">Interval for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">Interval for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="intervals">Intervals for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="intervals">Intervals for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini price ticker updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#all-market-mini-tickers-stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<AsterMiniTickUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#individual-symbol-mini-ticker-stream" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#individual-symbol-mini-ticker-stream" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#all-market-tickers-streams" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<AsterTickerUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#individual-symbol-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#all-book-tickers-stream" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#individual-symbol-book-ticker-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbol, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to liquidation updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#all-market-liquidation-order-streams" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(Action<DataEvent<AsterLiquidationUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to liquidation updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#liquidation-order-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(string symbol, Action<DataEvent<AsterLiquidationUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to liquidation updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#liquidation-order-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToLiquidationUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterLiquidationUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot order book updates for the top x rows
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="levels">Number of rows, 5, 10 or 20</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot order book updates for the top x rows
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#partial-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="levels">Number of rows, 5, 10 or 20</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book difference updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#diff-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book difference updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#diff-book-depth-streams" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100, 250 or 500</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="IAsterRestClientFuturesApiAccount.StartUserStreamAsync(CancellationToken)">restClient.FuturesApi.Account.StartUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#event-user-data-stream-expired" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IAsterRestClientFuturesApiAccount.StartUserStreamAsync(CancellationToken)">restClient.FuturesApi.Account.StartUserStreamAsync</see> method</param>
        /// <param name="onConfigUpdate">The event handler for leverage changed update</param>
        /// <param name="onMarginUpdate">The event handler for whenever a margin has changed</param>
        /// <param name="onAccountUpdate">The event handler for whenever an account update is received</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="onListenKeyExpired">Responds when the listen key for the stream has expired. Initiate a new instance of the stream here</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<AsterConfigUpdate>>? onConfigUpdate = null,
            Action<DataEvent<AsterMarginUpdate>>? onMarginUpdate = null,
            Action<DataEvent<AsterAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<AsterOrderUpdate>>? onOrderUpdate = null,
            Action<DataEvent<AsterSocketEvent>>? onListenKeyExpired = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IAsterSocketClientFuturesApiShared SharedClient { get; }
    }
}
