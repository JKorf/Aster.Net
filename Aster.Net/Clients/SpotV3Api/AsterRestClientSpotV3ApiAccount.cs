using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotApi;
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
        public async Task<WebCallResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("symbol", symbol);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/commissionRate", AsterExchange.RateLimiter.RestIp, 20, true);
            return await _baseClient.SendAsync<AsterUserCommission>(request, parameters, ct).ConfigureAwait(false);
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

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v3/asset/wallet/transfer", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<AsterSpotAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v3/account", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterSpotAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
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

            var request = _definitions.GetOrCreate(HttpMethod.Put, "api/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v3/listenKey", AsterExchange.RateLimiter.RestIp, 1, true);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
