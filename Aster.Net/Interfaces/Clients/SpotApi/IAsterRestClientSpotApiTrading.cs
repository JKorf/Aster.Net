using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Aster Spot trading endpoints, placing and managing orders.
    /// </summary>
    public interface IAsterRestClientSpotApiTrading
    {
        /// <summary>
        /// Place a new order
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#place-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="side">Order side</param>
        /// <param name="type">Order type</param>
        /// <param name="quantity">Quantity in base asset</param>
        /// <param name="quoteQuantity">Quantity in quote asset</param>
        /// <param name="clientOrderId">Client order id</param>
        /// <param name="price">Limit price</param>
        /// <param name="timeInForce">Time in force</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
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
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#cancel-order-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="orderId">Cancel by order id</param>
        /// <param name="clientOrderId">Cancel by client order id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get info on an order
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#query-order-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="orderId">Cancel by order id</param>
        /// <param name="clientOrderId">Cancel by client order id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get current open order list
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#current-open-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Cancel all open orders for a symbol
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#cancel-all-open-orders-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CancelAllOrdersAsync(
            string symbol,
            long? receiveWindow = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get list of all orders
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#query-all-orders-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Return orders after this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
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
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#account-trade-history-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="orderId">Filter by order id</param>
        /// <param name="fromId">Return orders after this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotUserTrade[]>> GetUserTradesAsync(string? symbol = null, long? orderId = null, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);
    }
}
