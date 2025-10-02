using Aster.Net.Clients.FuturesApi;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using System;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotApi
{
    /// <inheritdoc />
    internal class AsterRestClientSpotApiAccount : IAsterRestClientSpotApiAccount
    {
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();
        private readonly AsterRestClientSpotApi _baseClient;

        internal AsterRestClientSpotApiAccount(AsterRestClientSpotApi baseClient)
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

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/commissionRate", AsterExchange.RateLimiter.RestIp, 20, true);
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

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/asset/wallet/transfer", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Send To Address

        /// <inheritdoc />
        public async Task<WebCallResult<AsterTransferResult>> SendToAddressAsync(string asset, string toAddress, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("asset", asset);
            parameters.Add("amount", quantity);
            parameters.Add("toAddress", toAddress);
            parameters.Add("clientTranId", clientOrderId ?? Guid.NewGuid().ToString());
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Post, "/api/v1/asset/sendToAddress", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterTransferResult>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Withdraw Fee

        /// <inheritdoc />
        public async Task<WebCallResult<AsterWithdrawFee>> GetWithdrawFeeAsync(string asset, NetworkType network, long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.Add("asset", asset);
            parameters.AddEnumAsInt("chainId", network);
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/aster/withdraw/estimateFee", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterWithdrawFee>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Account Info

        /// <inheritdoc />
        public async Task<WebCallResult<AsterSpotAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("recvWindow", receiveWindow?.ToString(CultureInfo.InvariantCulture) ?? _baseClient.ClientOptions.ReceiveWindow.TotalMilliseconds.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/api/v1/account", AsterExchange.RateLimiter.RestIp, 5, true);
            return await _baseClient.SendAsync<AsterSpotAccountInfo>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Start User Data Stream
        /// <inheritdoc />
        public async Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Post, "api/v1/listenKey", AsterExchange.RateLimiter.RestIp, 1);
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

            var request = _definitions.GetOrCreate(HttpMethod.Put, "api/v1/listenKey", AsterExchange.RateLimiter.RestIp, 1);
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

            var request = _definitions.GetOrCreate(HttpMethod.Delete, "api/v1/listenKey", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion
    }
}
