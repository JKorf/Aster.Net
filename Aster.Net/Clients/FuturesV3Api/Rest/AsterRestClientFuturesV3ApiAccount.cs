using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using Aster.Net.Utils;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using System;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesV3Api
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
        public async Task<HttpResult> SetPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "dualSidePosition", dualPositionSide.ToString().ToLower() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/positionSide/dual", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Position Mode

        /// <inheritdoc />
        public async Task<HttpResult<AsterPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/positionSide/dual", AsterExchange.RateLimiter.RestIp, 30, true);
            return await _baseClient.SendAsync<AsterPositionMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Multi Asset Mode

        /// <inheritdoc />
        public async Task<HttpResult> SetMultiAssetModeAsync(bool multiAssetMargin, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "multiAssetsMargin", multiAssetMargin.ToString().ToLower() }
            };
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/multiAssetsMargin", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Multi Asset Mode

        /// <inheritdoc />
        public async Task<HttpResult<AsterMultiAssetMode>> GetMultiAssetModeAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/multiAssetsMargin", AsterExchange.RateLimiter.RestIp, 30, true);
            return await _baseClient.SendAsync<AsterMultiAssetMode>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Transfer

        /// <inheritdoc />
        public async Task<HttpResult<AsterTransferResult>> TransferAsync(string asset, TransferDirection direction, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("asset", asset);
            parameters.Add("amount", quantity);
            parameters.Add("kindType", direction);
            parameters.Add("clientTranId", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/asset/wallet/transfer", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Balances

        /// <inheritdoc />
        public async Task<HttpResult<AsterBalance[]>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/balance", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterBalance[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<HttpResult<AsterAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/accountWithJoinMargin", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Leverage

        /// <inheritdoc />
        public async Task<HttpResult<AsterLeverage>> SetLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("leverage", leverage);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/leverage", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterLeverage>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Set Margin Type

        /// <inheritdoc />
        public async Task<HttpResult> SetMarginTypeAsync(string symbol, MarginType marginType, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("marginType", marginType);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/marginType", AsterExchange.RateLimiter.RestIp, 5, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Modify Isolated Margin

        /// <inheritdoc />
        public async Task<HttpResult> ModifyIsolatedMarginAsync(string symbol, MarginAdjustSide side, decimal quantity, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", side);
            parameters.Add("amount", quantity);
            parameters.Add("positionSide", positionSide);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/positionMargin", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Position Margin Change History

        /// <inheritdoc />
        public async Task<HttpResult<AsterMarginChange[]>> GetPositionMarginChangeHistoryAsync(string symbol, MarginAdjustSide? side = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", side, EnumSerialization.Number);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/positionMargin/history", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterMarginChange[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Income History

        /// <inheritdoc />
        public async Task<HttpResult<AsterIncome[]>> GetIncomeHistoryAsync(string? symbol = null, IncomeType? type = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.Add("type", type);
            parameters.Add("startTime", startTime);
            parameters.Add("endTime", endTime);
            parameters.Add("limit", limit);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/income", AsterExchange.RateLimiter.RestIp, 30, true);
            return await _baseClient.SendAsync<AsterIncome[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Leverage Brackets

        /// <inheritdoc />
        public async Task<HttpResult<AsterSymbolBracket[]>> GetLeverageBracketsAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/leverageBracket", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterSymbolBracket[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Position ADL Quantile Estimations

        /// <inheritdoc />
        public async Task<HttpResult<AsterQuantileEstimation[]>> GetPositionAdlQuantileEstimationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            if (symbol == null)
            {
                var request1 = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/adlQuantile", AsterExchange.RateLimiter.RestIp, 5, true);
                return await _baseClient.SendAsync<AsterQuantileEstimation[]>(request1, parameters, ct).ConfigureAwait(false);
            }

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/adlQuantile", AsterExchange.RateLimiter.RestIp, 5, true);
            var result = await _baseClient.SendAsync<AsterQuantileEstimation>(request, parameters, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<AsterQuantileEstimation[]>(result);

            return HttpResult.Ok(result, new[] { result.Data });
        }

        #endregion

        #region Get User Commission Rate

        /// <inheritdoc />
        public async Task<HttpResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/fapi/v3/commissionRate", AsterExchange.RateLimiter.RestIp, 20, true);
            return await _baseClient.SendAsync<AsterUserCommission>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<HttpResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterListenKey>(request, null, ct).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail<string>(result);

            return HttpResult.Ok(result, result.Data?.ListenKey!);
        }

        #endregion

        #region Keepalive User Data Stream

        /// <inheritdoc />
        public async Task<HttpResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));

            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "fapi/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Close User Data Stream

        /// <inheritdoc />
        public async Task<HttpResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default)
        {
            listenKey.ValidateNotNull(nameof(listenKey));
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings)
            {
                { "listenKey", listenKey }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"fapi/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Approve Builder

        /// <inheritdoc />
        public Task<HttpResult> ApproveBuilderAsync(CancellationToken ct = default)
            => ApproveBuilderAsync(_baseClient.ClientOptions.BuilderAddress, (_baseClient.ClientOptions.BuilderFeePercentage ?? 0.01m) / 100, ct);

        /// <inheritdoc />
        public async Task<HttpResult> ApproveBuilderAsync(string builderAddress, decimal maxFeeRate, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("builder", builderAddress);
            parameters.Add("maxFeeRate", maxFeeRate, DecimalSerialization.String);
            parameters.Add("signaction", "ApproveBuilder");

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/approveBuilder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Update Builder

        /// <inheritdoc />
        public async Task<HttpResult> UpdateBuilderAsync(string builderAddress, decimal newMaxFeeRate, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("builder", builderAddress);
            parameters.Add("maxFeeRate", newMaxFeeRate / 100, DecimalSerialization.String);
            parameters.Add("signaction", "UpdateBuilder");

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/updateBuilder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Delete Builder

        /// <inheritdoc />
        public async Task<HttpResult> DeleteBuilderAsync(string builderAddress, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("builder", builderAddress);
            parameters.Add("signaction", "DelBuilder");

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"fapi/v3/builder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Approved Builders

        /// <inheritdoc />
        public async Task<HttpResult<AsterBuilder[]>> GetApprovedBuildersAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/builder", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterBuilder[]>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
        }

        #endregion

        #region Create Or Approve Builder

        /// <inheritdoc />
        public async Task<HttpResult> CreateOrApproveAgentAsync(
            string agentName,
            string agentAddress,
            string? ipWhitelist,
            DateTime? expireTime,
            bool canSpotTrade,
            bool canPerpTrade,
            bool canWithdraw,
            string? builder = null,
            decimal? builderMaxFeeRate = null,
            string? builderName = null,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("agentName", agentName);
            parameters.Add("agentAddress", agentAddress);
            parameters.Add("ipWhitelist", ipWhitelist ?? "");
            parameters.Add("expired", expireTime ?? DateTime.UtcNow.AddYears(5));
            parameters.Add("canSpotTrade", canSpotTrade);
            parameters.Add("canPerpTrade", canPerpTrade);
            parameters.Add("canWithdraw", canWithdraw);
            parameters.Add("builder", builder);
            parameters.Add("maxFeeRate", builderMaxFeeRate, DecimalSerialization.String);
            parameters.Add("builderName", builderName);

            parameters.Add("signaction", "ApproveAgent");

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/approveAgent", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Update Agent

        /// <inheritdoc />
        public async Task<HttpResult> UpdateAgentAsync(
            string agentAddress,
            string? ipWhitelist,
            bool canSpotTrade,
            bool canPerpTrade,
            bool canWithdraw,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("agentAddress", agentAddress);
            parameters.Add("ipWhitelist", ipWhitelist ?? "");
            parameters.Add("canSpotTrade", canSpotTrade);
            parameters.Add("canPerpTrade", canPerpTrade);
            parameters.Add("canWithdraw", canWithdraw);

            parameters.Add("signaction", "UpdateAgent");

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"fapi/v3/updateAgent", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Delete Agent

        /// <inheritdoc />
        public async Task<HttpResult> DeleteAgentAsync(
            string agentAddress,
            CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("agentAddress", agentAddress);

            parameters.Add("signaction", "DelAgent");

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"fapi/v3/agent", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterResult>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            if (result.Data.Code != 200)
                return HttpResult.Fail(result, new ServerError(result.Data.Code, _baseClient.GetErrorInfo(result.Data.Code, result.Data.Message)));

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Agents

        /// <inheritdoc />
        public async Task<HttpResult<AsterAgent[]>> GetAgentsAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"fapi/v3/agent", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterAgent[]>(request, null, ct, checkBuilderFee: false).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Withdraw Info

        /// <inheritdoc />
        public async Task<HttpResult<AsterWithdrawInfo>> GetWithdrawInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/aster/user-withdraw-info", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterWithdrawInfo>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

        #region Get Deposit Withdraw History

        /// <inheritdoc />
        public async Task<HttpResult<AsterDepositWithdrawal[]>> GetDepositWithdrawHistoryAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/fapi/v3/aster/deposit-withdraw-history", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterDepositWithdrawal[]>(request, null, ct).ConfigureAwait(false);
            return result;
        }

        #endregion

    }
}
