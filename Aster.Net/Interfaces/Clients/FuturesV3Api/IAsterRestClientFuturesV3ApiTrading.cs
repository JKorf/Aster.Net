using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.FuturesV3Api
{
    /// <summary>
    /// Aster Futures trading endpoints, placing and managing orders.
    /// </summary>
    public interface IAsterRestClientFuturesV3ApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#new-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Order quantity</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="reduceOnly">["<c>reduceOnly</c>"] Reduce only order</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Stop price</param>
        /// <param name="closePosition">["<c>closePosition</c>"] Close full position flag</param>
        /// <param name="activationPrice">["<c>activationPrice</c>"] Activation price</param>
        /// <param name="callbackRate">["<c>callbackRate</c>"] Callback rate</param>
        /// <param name="workingType">["<c>workingType</c>"] Working price type</param>
        /// <param name="priceProtect">["<c>priceProtect</c>"] Price protect</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
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
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#place-multiple-orders-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/batchOrders
        /// </para>
        /// </summary>
        /// <param name="orders">Orders to place</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<AsterOrder>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<AsterOrderRequest> orders,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get order info by id or clientOrderId
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#query-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Get by order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Get by clientOrderId, either this or orderId should be provided</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
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
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#cancel-order-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v3/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Get by order id, either this or clientOrderId should be provided</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Get by clientOrderId, either this or orderId should be provided</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
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
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#cancel-all-open-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v3/allOpenOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(
            string symbol,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel multiple orders in a single call
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="orderIdList">["<c>orderIdList</c>"] List or order ids</param>
        /// <param name="clientOrderIdList">["<c>origClientOrderIdList</c>"] List of client order ids</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<CallResult<AsterOrderResult>[]>> CancelMultipleOrdersAsync(string symbol, IEnumerable<long>? orderIdList = null, IEnumerable<string>? clientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders on a symbol when the timeout expires. Can be called on an interval to act as a dead man switch
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#cancel-multiple-orders-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/countdownCancelAll
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="countDownTime">["<c>countdownTime</c>"] Timeout time</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of current open orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#query-current-open-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get list of all orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#current-all-open-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/allOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Return orders after this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
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
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#position-information-v3-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v2/positionRisk
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Filter by symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPosition[]>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#account-trade-list-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/userTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get users forced orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#users-force-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/forceOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="closeType">["<c>autoCloseType</c>"] Filter by close type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 100</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrder[]>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default);
    }
}
