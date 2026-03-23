using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class AsterRestClientFuturesApiTrading : IAsterRestClientFuturesApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly AsterRestClientFuturesApi _baseClient;
        private readonly ILogger _logger;

        internal AsterRestClientFuturesApiTrading(ILogger logger, AsterRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrder>> PlaceOrderAsync(
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
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddEnum("side", side);
            parameters.AddEnum("type", type);
            parameters.AddOptional("quantity", quantity);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.AddOptionalEnum("timeInForce", timeInForce);
            parameters.AddOptional("reduceOnly", reduceOnly);
            parameters.AddOptional("price", price);
            parameters.AddOptional("newClientOrderId",clientOrderId);
            parameters.AddOptional("stopPrice", stopPrice);
            parameters.AddOptional("closePosition", closePosition);
            parameters.AddOptional("activationPrice", activationPrice);
            parameters.AddOptional("callbackRate", callbackRate);
            parameters.AddOptionalEnum("workingType", workingType);
            parameters.AddOptional("priceProtect", priceProtect);
            parameters.AddOptional("newOrderRespType", "RESULT");
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Multiple New Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<AsterOrder>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<AsterOrderRequest> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var parameterOrders = new List<Dictionary<string, object>>();
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new ParameterCollection()
                {
                    { "symbol", order.Symbol },
                    { "newOrderRespType", "RESULT" }
                };
                orderParameters.AddEnum("side", order.Side);
                orderParameters.AddEnum("type", order.Type);
                orderParameters.AddOptionalParameter("quantity", order.Quantity.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", order.ClientOrderId);
                orderParameters.AddOptionalEnum("timeInForce", order.TimeInForce);
                orderParameters.AddOptionalEnum("positionSide", order.PositionSide);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalEnum("workingType", order.WorkingType);
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                orderParameters.AddOptionalParameter("priceProtect", order.PriceProtect?.ToString().ToUpper());
                parameterOrders.Add(orderParameters);
                i++;
            }

#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
            parameters.Add("batchOrders", JsonSerializer.Serialize(parameterOrders, AsterExchange._serializerContext));
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/batchOrders", AsterExchange.RateLimiter.RestIp, 5, true);
            var response = await _baseClient.SendAsync<AsterOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return response.As<CallResult<AsterOrder>[]>(default);

            var result = new List<CallResult<AsterOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<AsterOrder>(new ServerError(item.Code.ToString(), _baseClient.GetErrorInfo(item.Code, item.Message)))
                    : new CallResult<AsterOrder>(item));
            }

            if (result.All(x => !x.Success))
                return response.AsErrorWithData(new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")), result.ToArray());

            return response.As(result.ToArray());
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrder>> GetOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrder>> CancelOrderAsync(
            string symbol,
            long? orderId = null,
            string? clientOrderId = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptional("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel All Order

        /// <inheritdoc />
        public async Task<WebCallResult> CancelAllOrdersAsync(
            string symbol,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/allOpenOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<WebCallResult<CallResult<AsterOrderResult>[]>> CancelMultipleOrdersAsync(string symbol, IEnumerable<long>? orderIdList = null, IEnumerable<string>? clientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderIdList == null && clientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or clientOrderIdList must be sent");

            if (orderIdList?.Count() > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (clientOrderIdList?.Count() > 10)
                throw new ArgumentException("clientOrderIdList cannot contain more than 10 items");

            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (clientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", clientOrderIdList!.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v1/batchOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            var response = await _baseClient.SendAsync<AsterOrderResult[]>(request, parameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return response.As<CallResult<AsterOrderResult>[]>(default);

            var result = new List<CallResult<AsterOrderResult>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? new CallResult<AsterOrderResult>(new ServerError(item.Code.ToString(), _baseClient.GetErrorInfo(item.Code, item.Message)))
                    : new CallResult<AsterOrderResult>(item));
            }

            return response.As(result.ToArray());
        }

        #endregion

        #region Auto-Cancel All Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<AsterCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol },
                { "countdownTime", (int)countDownTime.TotalSeconds }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v1/countdownCancelAll", AsterExchange.RateLimiter.RestIp, 10, true);
            return await _baseClient.SendAsync<AsterCountDownResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/openOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder[]>(request, parameters, ct, weight: weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrder[]>> GetOrdersAsync(
            string symbol,
            long? orderId = null,
            DateTime? startTime = null,
            DateTime? endTime = null,
            int? limit = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptional("orderId", orderId);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/allOrders", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<WebCallResult<AsterPosition[]>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v2/positionRisk", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<WebCallResult<AsterUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("fromId", fromId);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/userTrades", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Forced Orders

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrder[]>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalEnum("autoCloseType", closeType);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);

            var weight = symbol == null ? 50 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/forceOrders", AsterExchange.RateLimiter.RestIp, weight, true);
            return await _baseClient.SendAsync<AsterOrder[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion
    }
}
