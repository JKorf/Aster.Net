using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Sockets;
using Aster.Net.Objects.Models;
using System.Collections.Generic;
using Aster.Net.Enums;

namespace Aster.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Aster Spot streams
    /// </summary>
    public interface IAsterSocketClientSpotApi : ISocketApiClient, IDisposable
    {

        /// <summary>
        /// Subscribe to aggregated trade updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#collection-transaction-flow" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(string symbol, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to aggregated trade updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#collection-transaction-flow" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToAggregatedTradeUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterAggregatedTradeUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#k-line-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="interval">Interval for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#k-line-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="interval">Interval for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, KlineInterval interval, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#k-line-streams" /></para>
        /// </summary>
        /// <param name="symbol">The symbol</param>
        /// <param name="intervals">Intervals for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to kline/candlestick updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#k-line-streams" /></para>
        /// </summary>
        /// <param name="symbols">The symbols</param>
        /// <param name="intervals">Intervals for the klines</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(IEnumerable<string> symbols, IEnumerable<KlineInterval> intervals, Action<DataEvent<AsterKlineUpdate>> onMessage, CancellationToken ct = default);


        /// <summary>
        /// Subscribe to mini price ticker updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#simplified-ticker-by-symbol" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(Action<DataEvent<AsterMiniTickUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#simplified-ticker-by-symbol" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(string symbol, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to mini price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#simplified-ticker-by-symbol" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToMiniTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterMiniTickUpdate>> onMessage, CancellationToken ct = default);


        /// <summary>
        /// Subscribe to price ticker updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#full-ticker-per-symbol" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<AsterTickerUpdate[]>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#full-ticker-per-symbol" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to price ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#full-ticker-per-symbol" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(IEnumerable<string> symbols, Action<DataEvent<AsterTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#best-order-book-information-by-symbol" /></para>
        /// </summary>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#best-order-book-information-by-symbol" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(string symbol, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to book ticker updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#best-order-book-information-by-symbol" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToBookTickerUpdatesAsync(IEnumerable<string> symbol, Action<DataEvent<AsterBookTickerUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot order book updates for the top x rows
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#limited-depth-information" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="levels">Number of rows, 5, 10 or 20</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(string symbol, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to snapshot order book updates for the top x rows
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#limited-depth-information" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="levels">Number of rows, 5, 10 or 20</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdatesAsync(IEnumerable<string> symbols, int levels, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book difference updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#incremental-depth-information" /></para>
        /// </summary>
        /// <param name="symbol">Symbol</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string symbol, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribe to order book difference updates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#incremental-depth-information" /></para>
        /// </summary>
        /// <param name="symbols">Symbols</param>
        /// <param name="updateInterval">Update interval in milliseconds, either 100 or 1000</param>
        /// <param name="onMessage">The event handler for the received data</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(IEnumerable<string> symbols, int? updateInterval, Action<DataEvent<AsterOrderBookUpdate>> onMessage, CancellationToken ct = default);

        /// <summary>
        /// Subscribes to the account update stream. Prior to using this, the <see cref="IAsterRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method should be called to start the stream and obtaining a listen key.
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#payload-account_update" /></para>
        /// </summary>
        /// <param name="listenKey">Listen key retrieved by the <see cref="IAsterRestClientSpotApiAccount.StartUserStreamAsync(CancellationToken)">restClient.SpotApi.Account.StartUserStreamAsync</see> method</param>
        /// <param name="onAccountUpdate">The event handler for whenever an account update is received</param>
        /// <param name="onOrderUpdate">The event handler for whenever an order status update is received</param>
        /// <param name="ct">Cancellation token for closing this subscription</param>
        /// <returns>A stream subscription. This stream subscription can be used to be notified when the socket is disconnected/reconnected</returns>
        Task<CallResult<UpdateSubscription>> SubscribeToUserDataUpdatesAsync(
            string listenKey,
            Action<DataEvent<AsterSpotAccountUpdate>>? onAccountUpdate = null,
            Action<DataEvent<AsterSpotOrderUpdate>>? onOrderUpdate = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get the shared socket requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IAsterSocketClientSpotApiShared SharedClient { get; }
    }
}
