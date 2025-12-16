using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Aster Spot account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IAsterRestClientSpotApiAccount
    {
        /// <summary>
        /// Get user fee rates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#get-symbol-fees" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between Spot and Futures account
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#perp-spot-transfer-trade" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="direction">Transfer direction</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="clientOrderId">Client defined id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTransferResult>> TransferAsync(string asset, TransferDirection direction, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Send to a different address
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#transfer-asset-to-other-address-trade" /></para>
        /// </summary>
        /// <param name="asset">Asset to send</param>
        /// <param name="toAddress">Target EVM address</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="clientOrderId">Client defined id, min 20 characters</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTransferResult>> SendToAddressAsync(string asset, string toAddress, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal fee
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#get-withdraw-fee-none" /></para>
        /// </summary>
        /// <param name="asset">Asset to withdraw</param>
        /// <param name="network">Network type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterWithdrawFee>> GetWithdrawFeeAsync(string asset, NetworkType network, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account info and balances
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#account-information-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Start a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client. The stream will close after 60 minutes unless <see cref="KeepAliveUserStreamAsync">KeepAliveUserStreamAsync</see> is called.
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#generate-listen-key-user_stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Keep alive the user stream. This should be called every 30 minutes to prevent the user stream being stopped
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#extend-listen-key-validity-period-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stop the user stream, no updates will be send anymore
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#close-listen-key-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);
    }
}
