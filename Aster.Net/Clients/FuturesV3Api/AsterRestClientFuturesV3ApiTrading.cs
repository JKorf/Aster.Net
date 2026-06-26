using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using Aster.Net.Utils;
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

namespace Aster.Net.Clients.FuturesV3Api
{
    /// <inheritdoc />
    internal class AsterRestClientFuturesV3ApiTrading : IAsterRestClientFuturesV3ApiTrading
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly AsterRestClientFuturesV3Api _baseClient;
        private readonly ILogger _logger;

        internal AsterRestClientFuturesV3ApiTrading(ILogger logger, AsterRestClientFuturesV3Api baseClient)
        {
            _baseClient = baseClient;
            _logger = logger;
        }

        #region Place Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder>> PlaceOrderAsync(
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
            PegPriceType? pegPriceType = null,
            decimal? pegOffset = null,
            decimal? priceLimit = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("side", side);
            parameters.Add("type", type);
            parameters.Add("quantity", quantity);
            parameters.Add("positionSide", positionSide);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("reduceOnly", reduceOnly);
            parameters.Add("price", price);
            parameters.Add("newClientOrderId",clientOrderId);
            parameters.Add("stopPrice", stopPrice);
            parameters.Add("closePosition", closePosition);
            parameters.Add("activationPrice", activationPrice);
            parameters.Add("callbackRate", callbackRate);
            parameters.Add("workingType", workingType);
            parameters.Add("priceProtect", priceProtect);
            parameters.Add("pegPriceType", pegPriceType);
            parameters.Add("pegOffset", pegOffset);
            parameters.Add("priceLimit", priceLimit);
            parameters.Add("newOrderRespType", "RESULT");

            if (_baseClient.ClientOptions.BuilderFeePercentage > 0
                && _baseClient.ClientOptions.BuilderAddress != null
                && AsterUtils._builderFeeSuccess)
            {
                parameters.Add("builder", _baseClient.ClientOptions.BuilderAddress);
                parameters.Add("feeRate", _baseClient.ClientOptions.BuilderFeePercentage / 100);
            }
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Multiple New Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<AsterOrder>[]>> PlaceMultipleOrdersAsync(
            IEnumerable<AsterOrderRequest> orders,
            int? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            var parameterOrders = new List<Parameters>();
            int i = 0;
            foreach (var order in orders)
            {
                var orderParameters = new Parameters(AsterExchange._parameterSerializationSettings)
                {
                    { "symbol", order.Symbol },
                    { "newOrderRespType", "RESULT" }
                };
                orderParameters.Add("side", order.Side);
                orderParameters.Add("type", order.Type);
                orderParameters.AddOptionalParameter("quantity", order.Quantity.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("newClientOrderId", order.ClientOrderId);
                orderParameters.Add("timeInForce", order.TimeInForce);
                orderParameters.Add("positionSide", order.PositionSide);
                orderParameters.AddOptionalParameter("price", order.Price?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("stopPrice", order.StopPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("activationPrice", order.ActivationPrice?.ToString(CultureInfo.InvariantCulture));
                orderParameters.AddOptionalParameter("callbackRate", order.CallbackRate?.ToString(CultureInfo.InvariantCulture));
                orderParameters.Add("workingType", order.WorkingType);
                orderParameters.AddOptionalParameter("reduceOnly", order.ReduceOnly?.ToString().ToLower());
                orderParameters.AddOptionalParameter("priceProtect", order.PriceProtect?.ToString().ToUpper());
                if (_baseClient.ClientOptions.BuilderFeePercentage > 0
                    && _baseClient.ClientOptions.BuilderAddress != null
                    && AsterUtils._builderFeeSuccess)
                {
                    orderParameters.Add("builder", _baseClient.ClientOptions.BuilderAddress);
                    orderParameters.Add("feeRate", _baseClient.ClientOptions.BuilderFeePercentage / 100);
                }
                parameterOrders.Add(orderParameters);
                i++;
            }

#pragma warning disable IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
#pragma warning disable IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
            parameters.Add("batchOrders", JsonSerializer.Serialize(parameterOrders, AsterExchange._serializerContext));
#pragma warning restore IL3050 // Calling members annotated with 'RequiresDynamicCodeAttribute' may break functionality when AOT compiling.
#pragma warning restore IL2026 // Members annotated with 'RequiresUnreferencedCodeAttribute' require dynamic access otherwise can break functionality when trimming application code
            
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/batchOrders", AsterExchange.RateLimiter.RestIp, 5, true);
            var response = await _baseClient.SendAsync<AsterOrderResult[]>(request, parameters, ct).ConfigureAwait(false);
            if (!response.Success)
                return HttpResult.Fail<CallResult<AsterOrder>[]>(response);

            var result = new List<CallResult<AsterOrder>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? CallResult<AsterOrder>.Fail(new ServerError(item.Code.ToString(), _baseClient.GetErrorInfo(item.Code, item.Message)))
                    : CallResult<AsterOrder>.Ok(item));
            }

            if (result.All(x => !x.Success))
                return HttpResult.Fail(response, new ServerError(new ErrorInfo(ErrorType.AllOrdersFailed, false, "All orders failed")), result.ToArray());

            return HttpResult.Ok(response, result.ToArray());
        }

        #endregion

        #region Edit Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder>> EditOrderAsync(
            string symbol,
            long? orderId,
            string? clientOrderId,
            decimal quantity,
            decimal price,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };
            parameters.Add("quantity", quantity);
            parameters.Add("price", price);
            parameters.Add("orderId", orderId);
            parameters.Add("origClientOrderId", clientOrderId);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "fapi/v3/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder>> GetOrderAsync(
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Cancel Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder>> CancelOrderAsync(
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"fapi/v3/order", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"fapi/v3/allOpenOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Cancel Multiple Orders

        /// <inheritdoc />
        public async Task<HttpResult<CallResult<AsterOrderResult>[]>> CancelMultipleOrdersAsync(string symbol, IEnumerable<long>? orderIdList = null, IEnumerable<string>? clientOrderIdList = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            if (orderIdList == null && clientOrderIdList == null)
                throw new ArgumentException("Either orderIdList or clientOrderIdList must be sent");

            if (orderIdList?.Count() > 10)
                throw new ArgumentException("orderIdList cannot contain more than 10 items");

            if (clientOrderIdList?.Count() > 10)
                throw new ArgumentException("clientOrderIdList cannot contain more than 10 items");

            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol }
            };

            if (orderIdList != null)
                parameters.AddOptionalParameter("orderIdList", $"[{string.Join(",", orderIdList)}]");

            if (clientOrderIdList != null)
                parameters.AddOptionalParameter("origClientOrderIdList", $"[{string.Join(",", clientOrderIdList!.Select(id => $"\"{id}\""))}]");

            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"fapi/v3/batchOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            var response = await _baseClient.SendAsync<AsterOrderResult[]>(request, parameters, ct).ConfigureAwait(false);

            if (!response.Success)
                return HttpResult.Fail<CallResult<AsterOrderResult>[]>(response);

            var result = new List<CallResult<AsterOrderResult>>();
            foreach (var item in response.Data)
            {
                result.Add(item.Code != 0
                    ? CallResult<AsterOrderResult>.Fail(new ServerError(item.Code.ToString(), _baseClient.GetErrorInfo(item.Code, item.Message)))
                    : CallResult<AsterOrderResult>.Ok(item));
            }

            return HttpResult.Ok(response, result.ToArray());
        }

        #endregion

        #region Auto-Cancel All Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<AsterCountDownResult>> CancelAllOrdersAfterTimeoutAsync(string symbol, TimeSpan countDownTime, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "symbol", symbol },
                { "countdownTime", (int)countDownTime.TotalSeconds }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/countdownCancelAll", AsterExchange.RateLimiter.RestIp, 10, true);
            return await _baseClient.SendAsync<AsterCountDownResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Open Orders

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder[]>> GetOpenOrdersAsync(
            string? symbol = null,
            long? receiveWindow = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var weight = symbol == null ? 40 : 1;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/openOrders", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterOrder[]>(request, parameters, ct, weight: weight).ConfigureAwait(false);
        }

        #endregion

        #region Get Orders

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder[]>> GetOrdersAsync(
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/allOrders", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterOrder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Positions

        /// <inheritdoc />
        public async Task<HttpResult<AsterPosition[]>> GetPositionsAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/positionRisk", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterPosition[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get User Trades

        /// <inheritdoc />
        public async Task<HttpResult<AsterUserTrade[]>> GetUserTradesAsync(string symbol, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("fromId", fromId);
            parameters.Add("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/userTrades", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterUserTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Forced Orders

        /// <inheritdoc />
        public async Task<HttpResult<AsterOrder[]>> GetForcedOrdersAsync(string? symbol = null, AutoCloseType? closeType = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, int? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.Add("autoCloseType", closeType);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);

            var weight = symbol == null ? 50 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/forceOrders", AsterExchange.RateLimiter.RestIp, weight, true);
            return await _baseClient.SendAsync<AsterOrder[]>(request, parameters, ct, weight).ConfigureAwait(false);
        }

        #endregion

        #region Place Chase Order

        /// <inheritdoc />
        public async Task<HttpResult<AsterStrategyOrderResult>> PlaceChaseOrderAsync(
            string symbol,
            OrderSide side,
            decimal quantity,
            QuantityUnit quantityUnit,
            PositionSide? positionSide = null,
            bool? reduceOnly = null,
            decimal? chaseOffset = null,
            ChaseOffsetType? chaseOffsetType = null,
            decimal? maxChaseOffset = null,
            ChaseOffsetType? maxChaseOffsetType = null,
            decimal? priceLimit = null,
            TimeInForce? timeInForce = null,
            string? clientOrderId = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("side", side);
            parameters.Add("quantity", quantity);
            parameters.Add("quantityUnit", quantityUnit);
            parameters.Add("positionSide", positionSide);
            parameters.Add("reduceOnly", reduceOnly);
            parameters.Add("chaseOffset", chaseOffset);
            parameters.Add("chaseOffsetType", chaseOffsetType);
            parameters.Add("maxChaseOffset", maxChaseOffset);
            parameters.Add("maxChaseOffsetType", maxChaseOffsetType);
            parameters.Add("priceLimit", priceLimit);
            parameters.Add("timeInForce", timeInForce);
            parameters.Add("clientStrategyId", clientOrderId);
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/chase", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterStrategyOrderResult>(request, parameters, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
