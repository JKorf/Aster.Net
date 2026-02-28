using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Aster Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IAsterRestClientFuturesApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#new-order--trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v1/order
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Order quantity</param>
        /// <param name="price">Limit price</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="reduceOnly">Reduce only order</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="closePosition">Close full position flag</param>
        /// <param name="activationPrice">Activation price</param>
        /// <param name="callbackRate">Callback rate</param>
        /// <param name="workingType">Working price type</param>
        /// <param name="priceProtect">Price protect</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder>> PlaceOrderAsync(
            string symbol,
            OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? price = null,
            PositionSide? positionSide = null,
            TimeInForce? timeInForce = null,
            bool? reduceOnly = null,
            string? clientOrderId = null,
            decimal? stopPrice = null,
            bool? closePosition = null,
            decimal? activationPrice = null,
            decimal? callbackRate = null,
            WorkingType? workingType = null,
            bool? priceProtect = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Place multiple new orders in a single call
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api-v3.md#place-multiple-orders--trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v1/batchOrders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<AsterOrder>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<AsterOrderRequest> orders,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get order info by id or clientOrderId
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#query-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v1/order
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Get by order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Get by clientOrderId, either this or orderId should be provided</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel and order by id or clientOrderId
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#cancel-order-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v1/order
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Get by order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">Get by clientOrderId, either this or orderId should be provided</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#cancel-all-open-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v1/allOpenOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(
            string symbol,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders in a single call
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="orderIdList">List or order ids</param>
        /// <param name="clientOrderIdList">List of client order ids</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<AsterOrderResult>[]>> CancelMultipleOrdersAsync(string symbol, IEnumerable<long>? orderIdList = null, IEnumerable<string>? clientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders on a symbol when the timeout expires. Can be called on an interval to act as a dead man switch
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#auto-cancel-all-open-orders-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v1/countdownCancelAll
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="countDownTime">Timeout time</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of current open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#current-all-open-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v1/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get list of all orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#all-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v1/allOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Return orders after this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder[]>> GetOrdersAsync(
            string symbol,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#position-information-v2-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v2/positionRisk
        /// </para>
        /// </summary>
        /// <param name="symbol">Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPosition[]>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#account-trade-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v1/userTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">Return results after this</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get users forced orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#users-force-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v1/forceOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="closeType">Filter by close type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 100</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder[]>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
    }
}
