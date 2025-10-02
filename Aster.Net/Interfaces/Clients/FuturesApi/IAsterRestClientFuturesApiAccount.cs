using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Aster Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IAsterRestClientFuturesApiAccount
    {
        /// <summary>
        /// Set the position mode for the whole account
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#change-position-modetrade" /></para>
        /// </summary>
        /// <param name="dualPositionSide">True: Hedge mode, False: One-way mode</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current account position mode
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#get-current-multi-assets-mode-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set multi asset margin mode
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api-v3.md#change-multi-assets-mode-trade" /></para>
        /// </summary>
        /// <param name="multiAssetMargin">Multi asset mode margin enabled or not</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetMultiAssetModeAsync(bool multiAssetMargin, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get multi asset margin mode
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api-v3.md#get-current-multi-assets-mode-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMultiAssetMode>> GetMultiAssetModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between Spot and Futures account
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api-v3.md#transfer-between-futures-and-spot-user_data" /></para>
        /// </summary>
        /// <param name="asset">Asset to transfer</param>
        /// <param name="direction">Transfer direction</param>
        /// <param name="quantity">Quantity</param>
        /// <param name="clientOrderId">Client defined id</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTransferResult>> TransferAsync(string asset, TransferDirection direction, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account balances
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#futures-account-balance-v2-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBalance[]>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account info, including assets and positions
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#account-information-v4-user_data" /></para>
        /// </summary>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set initial leverage
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#change-initial-leverage-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="leverage">Leverage</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterLeverage>> SetLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set margin type
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#change-margin-type-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="marginType">Margin type</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetMarginTypeAsync(string symbol, MarginType marginType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Modify isolated margin
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#modify-isolated-position-margin-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="side">Adjust side</param>
        /// <param name="quantity">Quantity to add or remove</param>
        /// <param name="positionSide">Position side</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ModifyIsolatedMarginAsync(string symbol, MarginAdjustSide side, decimal quantity, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position margin change history
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#get-position-margin-change-history-trade" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="side">Filter by adjust side</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMarginChange[]>> GetPositionMarginChangeHistoryAsync(string symbol, MarginAdjustSide? side = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get income history
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#get-income-historyuser_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="type">Filter by type</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results, max 1000</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterIncome[]>> GetIncomeHistoryAsync(string? symbol = null, IncomeType? type = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#notional-and-leverage-brackets-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSymbolBracket[]>> GetLeverageBracketsAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position ADL quantile estimations
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#position-adl-quantile-estimation-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterQuantileEstimation[]>> GetPositionAdlQuantileEstimationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user fee rates
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#user-commission-rate-user_data" /></para>
        /// </summary>
        /// <param name="symbol">Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Start a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client. The stream will close after 60 minutes unless <see cref="KeepAliveUserStreamAsync">KeepAliveUserStreamAsync</see> is called.
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#start-user-data-stream-user_stream" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Keep alive the user stream. This should be called every 30 minutes to prevent the user stream being stopped
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#keepalive-user-data-stream-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stop the user stream, no updates will be send anymore
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#close-user-data-stream-user_stream" /></para>
        /// </summary>
        /// <param name="listenKey">The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

    }
}
