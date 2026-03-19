using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class AsterRestClientFuturesV3ApiAccount : IAsterRestClientFuturesV3ApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly AsterRestClientFuturesV3Api _baseClient;

        internal AsterRestClientFuturesV3ApiAccount(AsterRestClientFuturesV3Api baseClient)
        {
            _baseClient = baseClient;
        }

        #region Set Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SetPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "dualSidePosition", dualPositionSide.ToString().ToLower() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v3/positionSide/dual", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<WebCallResult<AsterPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v3/positionSide/dual", AsterExchange.RateLimiter.RestIp, 30, true);
            return await _baseClient.SendAsync<AsterPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Multi Asset Mode

        /// <inheritdoc />
        public async Task<WebCallResult> SetMultiAssetModeAsync(bool multiAssetMargin, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "multiAssetsMargin", multiAssetMargin.ToString().ToLower() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v3/multiAssetsMargin", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Get Multi Asset Mode

        /// <inheritdoc />
        public async Task<WebCallResult<AsterMultiAssetMode>> GetMultiAssetModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/multiAssetsMargin", AsterExchange.RateLimiter.RestIp, 30, true);
            return await _baseClient.SendAsync<AsterMultiAssetMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<WebCallResult<AsterTransferResult>> TransferAsync(string asset, TransferDirection direction, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("asset", asset);
            parameters.Add("amount", quantity);
            parameters.AddEnum("kindType", direction);
            parameters.Add("clientTranId", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v3/asset/wallet/transfer", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<WebCallResult<AsterBalance[]>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/balance", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterBalance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<AsterAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/account", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<WebCallResult<AsterLeverage>> SetLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v3/leverage", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterLeverage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Type

        /// <inheritdoc />
        public async Task<WebCallResult> SetMarginTypeAsync(string symbol, MarginType marginType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnum("marginType", marginType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v3/marginType", AsterExchange.RateLimiter.RestIp, 5, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Modify Isolated Margin

        /// <inheritdoc />
        public async Task<WebCallResult> ModifyIsolatedMarginAsync(string symbol, MarginAdjustSide side, decimal quantity, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddEnumAsInt("type", side);
            parameters.Add("quantity", quantity);
            parameters.AddOptionalEnum("positionSide", positionSide);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/fapi/v3/positionMargin", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Get Position Margin Change History

        /// <inheritdoc />
        public async Task<WebCallResult<AsterMarginChange[]>> GetPositionMarginChangeHistoryAsync(string symbol, MarginAdjustSide? side = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalEnumAsInt("type", side);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/positionMargin/history", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterMarginChange[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Income History

        /// <inheritdoc />
        public async Task<WebCallResult<AsterIncome[]>> GetIncomeHistoryAsync(string? symbol = null, IncomeType? type = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalEnum("type", type);
            parameters.AddOptionalMilliseconds("startTime", startTime);
            parameters.AddOptionalMilliseconds("endTime", endTime);
            parameters.AddOptional("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/income", AsterExchange.RateLimiter.RestIp, 30, true);
            return await _baseClient.SendAsync<AsterIncome[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<WebCallResult<AsterSymbolBracket[]>> GetLeverageBracketsAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptional("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v3/leverageBracket", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterSymbolBracket[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Position ADL Quantile Estimations

        /// <inheritdoc />
        public async Task<WebCallResult<AsterQuantileEstimation[]>> GetPositionAdlQuantileEstimationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            if (symbol == null)
            {
                var request1 = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v3/adlQuantile", AsterExchange.RateLimiter.RestIp, 5, true);
                return await _baseClient.SendAsync<AsterQuantileEstimation[]>(request1, parameters, ct).ConfigureAwait(false);
            }

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v3/adlQuantile", AsterExchange.RateLimiter.RestIp, 5, true);
            var result = await _baseClient.SendAsync<AsterQuantileEstimation>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.As<AsterQuantileEstimation[]>(null);

            return result.As(new[] { result.Data });
        }

        #endregion

        #region Get User Commission Rate

        /// <inheritdoc />
        public async Task<WebCallResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v3/commissionRate", AsterExchange.RateLimiter.RestIp, 20, true);
            return await _baseClient.SendAsync<AsterUserCommission>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1);
            var result = await _baseClient.SendAsync<AsterListenKey>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ListenKey!);
        }

        #endregion

        #region Keepalive User Data Stream

        /// <inheritdoc />
        public async Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, "fapi/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Close User Data Stream

        /// <inheritdoc />
        public async Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new ParameterCollection
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Approve Builder

        /// <inheritdoc />
        public Task<WebCallResult> ApproveBuilderAsync(CancellationToken ct = default)
            => ApproveBuilderAsync(_baseClient.ClientOptions.BuilderAddress, _baseClient.ClientOptions.BuilderName, (_baseClient.ClientOptions.BuilderFeePercentage ?? 0.01m) / 100, ct);

        /// <inheritdoc />
        public async Task<WebCallResult> ApproveBuilderAsync(string builderAddress, string builderName, decimal maxFeeRate, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("builder", builderAddress);
            parameters.AddString("maxFeeRate", maxFeeRate);
            parameters.Add("builderName", builderName);
            parameters.Add("signaction", "ApproveBuilder");

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v3/approveBuilder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Update Builder

        /// <inheritdoc />
        public async Task<WebCallResult> UpdateBuilderAsync(string builderAddress, decimal newMaxFeeRate, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("builder", builderAddress);
            parameters.AddString("maxFeeRate", newMaxFeeRate / 100);
            parameters.Add("signaction", "UpdateBuilder");

            var request = _definitions.GetOrCreate(HttpMethod.Post, "fapi/v3/updateBuilder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Delete Builder

        /// <inheritdoc />
        public async Task<WebCallResult> DeleteBuilderAsync(string builderAddress, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("builder", builderAddress);
            parameters.Add("signaction", "DelBuilder");

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "fapi/v3/builder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result)
                return result.AsDataless();

            if (result.Data.Code != 200)
                return result.AsDatalessError(new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));
            return result.AsDataless();
        }

        #endregion

        #region Get Approved Builders

        /// <inheritdoc />
        public async Task<WebCallResult<AsterBuilder[]>> GetApprovedBuildersAsync(CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v3/builder", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterBuilder[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
