using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotV3Api
{
    /// <inheritdoc />
    internal class AsterRestClientSpotV3ApiAccount : IAsterRestClientSpotV3ApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly AsterRestClientSpotV3Api _baseClient;

        internal AsterRestClientSpotV3ApiAccount(AsterRestClientSpotV3Api baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get User Commission Rate

        /// <inheritdoc />
        public async Task<HttpResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/api/v3/commissionRate", AsterExchange.RateLimiter.RestIp, 20, true);
            return await _baseClient.SendAsync<AsterUserCommission>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"/api/v3/asset/wallet/transfer", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<HttpResult<AsterSpotAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress,"/api/v3/account", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterSpotAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<HttpResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress,"api/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync<AsterListenKey>(request, null, ct, checkBuilderFee: false).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Put, _baseClient.BaseAddress, "api/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress,"api/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/approveBuilder", AsterExchange.RateLimiter.RestIp, 5, true);
            var result = await _baseClient.SendAsync(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

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

            var request = _definitions.GetOrCreate(HttpMethod.Post, _baseClient.BaseAddress, "api/v3/updateBuilder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, _baseClient.BaseAddress, "api/v3/delBuilder", AsterExchange.RateLimiter.RestIp, 1, true);
            var result = await _baseClient.SendAsync(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
            if (!result.Success)
                return HttpResult.Fail(result);

            return HttpResult.Ok(result);
        }

        #endregion

        #region Get Approved Builders

        /// <inheritdoc />
        public async Task<HttpResult<AsterBuilder[]>> GetApprovedBuildersAsync(CancellationToken ct = default)
        {
            var parameters = new Parameters(AsterExchange._parameterSerializationSettings);
            var request = _definitions.GetOrCreate(HttpMethod.Get, _baseClient.BaseAddress, "api/v3/builder", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync<AsterBuilder[]>(request, parameters, ct, checkBuilderFee: false).ConfigureAwait(false);
        }

        #endregion
    }
}
