using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.SpotV3Api
{
    /// <summary>
    /// Aster Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IAsterRestClientSpotV3ApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#place-order-trade" /><br />
        /// Endpoint:<br />
        /// POST /api/v1/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="side">["<c>side</c>"] Order side</param>
        /// <param name="type">["<c>type</c>"] Order type</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity in base asset</param>
        /// <param name="quoteQuantity">["<c>quoteOrderQty</c>"] Quantity in quote asset</param>
        /// <param name="clientOrderId">["<c>newClientOrderId</c>"] Client order id</param>
        /// <param name="price">["<c>price</c>"] Limit price</param>
        /// <param name="timeInForce">["<c>timeInForce</c>"] Time in force</param>
        /// <param name="stopPrice">["<c>stopPrice</c>"] Stop price</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder>> PlaceOrderAsync(string symbol,
            Enums.OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? clientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            int? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#cancel-order-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Cancel by order id</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Cancel by client order id</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get info on an order
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#query-order-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/order
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Cancel by order id</param>
        /// <param name="clientOrderId">["<c>origClientOrderId</c>"] Cancel by client order id</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get current open order list
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#current-open-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/openOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#cancel-all-open-orders-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /api/v1/allOpenOrders
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
        /// Get list of all orders
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#query-all-orders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/allOrders
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Return orders after this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder[]>> GetOrdersAsync(
            string symbol,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get user trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#account-trade-history-user_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/userTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">["<c>orderId</c>"] Filter by order id</param>
        /// <param name="fromId">["<c>fromId</c>"] Return orders after this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotUserTrade[]>> GetUserTradesAsync(string? symbol = null, long? orderId = null, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
