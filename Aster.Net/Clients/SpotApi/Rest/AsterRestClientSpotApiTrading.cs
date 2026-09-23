using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class AsterRestClientSpotApiTrading : IAsterRestClientSpotApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly AsterRestClientSpotApi _baseClient;
        private readonly ILogger _logger;

        internal AsterRestClientSpotApiTrading(ILogger logger, AsterRestClientSpotApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region New Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotOrder>> PlaceOrderAsync(string symbol,
            Enums.OrderSide side,
            OrderType type,
            decimal? quantity = null,
            decimal? quoteQuantity = null,
            string? clientOrderId = null,
            decimal? price = null,
            TimeInForce? timeInForce = null,
            decimal? stopPrice = null,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity);
            parameters.Add("quoteOrderQty", quoteQuantity);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("price", price);
            parameters.Add("newClientOrderId", clientOrderId);
            parameters.Add("stopPrice", stopPrice);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"api/v1/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterSpotOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"api/v1/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterSpotOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"api/v1/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterSpotOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"api/v1/openOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterSpotOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Order

        /// <inheritdoc />
        public async Task<HttpResult> CancelAllOrdersAsync(
            string symbol,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"api/v1/allOpenOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotOrder[]>> GetOrdersAsync(
            string symbol,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("orderId", orderId);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"api/v1/allOrders", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterSpotOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotUserTrade[]>> GetUserTradesAsync(string? symbol = null, long? orderId = null, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("orderId", orderId);
            parameters.Add("fromId", fromId);
            parameters.Add("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/api/v1/userTrades", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterSpotUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
