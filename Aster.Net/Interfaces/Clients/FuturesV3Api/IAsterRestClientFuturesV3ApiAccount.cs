using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.FuturesV3Api
{
    /// <summary>
    /// Aster Futures account endpoints. Account endpoints include balance info, withdraw/deposit info and requesting and account settings
    /// </summary>
    public interface IAsterRestClientFuturesV3ApiAccount
    {
        /// <summary>
        /// Set the position mode for the whole account
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#change-position-modetrade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/positionSide/dual
        /// </para>
        /// </summary>
        /// <param name="dualPositionSide">["<c>dualSidePosition</c>"] True: Hedge mode, False: One-way mode</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetPositionModeAsync(bool dualPositionSide, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get the current account position mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#get-current-position-modeuser_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/positionSide/dual
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPositionMode>> GetPositionModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set multi asset margin mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#change-multi-assets-mode-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/multiAssetsMargin
        /// </para>
        /// </summary>
        /// <param name="multiAssetMargin">["<c>multiAssetsMargin</c>"] Multi asset mode margin enabled or not</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetMultiAssetModeAsync(bool multiAssetMargin, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get multi asset margin mode
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#get-current-multi-assets-mode-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/multiAssetsMargin
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMultiAssetMode>> GetMultiAssetModeAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Transfer between Spot and Futures account
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#transfer-between-futures-and-spot-transfer" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/asset/wallet/transfer
        /// </para>
        /// </summary>
        /// <param name="asset">["<c>asset</c>"] Asset to transfer</param>
        /// <param name="direction">["<c>kindType</c>"] Transfer direction</param>
        /// <param name="quantity">["<c>amount</c>"] Quantity</param>
        /// <param name="clientOrderId">["<c>clientTranId</c>"] Client defined id</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTransferResult>> TransferAsync(string asset, TransferDirection direction, decimal quantity, string? clientOrderId = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account balances
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#futures-account-balance-v3-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/balance
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBalance[]>> GetBalancesAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get account info, including assets and positions
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#account-information-v3-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/account
        /// </para>
        /// </summary>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAccountInfo>> GetAccountInfoAsync(long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set initial leverage
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#change-initial-leverage-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/leverage
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="leverage">["<c>leverage</c>"] Leverage</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterLeverage>> SetLeverageAsync(string symbol, int leverage, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Set margin type
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#change-margin-type-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/marginType
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="marginType">["<c>marginType</c>"] Margin type</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> SetMarginTypeAsync(string symbol, MarginType marginType, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Modify isolated margin
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#modify-isolated-position-margin-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/positionMargin
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>type</c>"] Adjust side</param>
        /// <param name="quantity">["<c>quantity</c>"] Quantity to add or remove</param>
        /// <param name="positionSide">["<c>positionSide</c>"] Position side</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ModifyIsolatedMarginAsync(string symbol, MarginAdjustSide side, decimal quantity, PositionSide? positionSide = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position margin change history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#get-position-margin-change-history-trade" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/positionMargin/history
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="side">["<c>type</c>"] Filter by adjust side</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMarginChange[]>> GetPositionMarginChangeHistoryAsync(string symbol, MarginAdjustSide? side = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get income history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#get-income-historyuser_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/income
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="type">["<c>type</c>"] Filter by type</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results, max 1000</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterIncome[]>> GetIncomeHistoryAsync(string? symbol = null, IncomeType? type = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get leverage brackets
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#notional-and-leverage-brackets-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/leverageBracket
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSymbolBracket[]>> GetLeverageBracketsAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get position ADL quantile estimations
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#position-adl-quantile-estimation-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/adlQuantile
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterQuantileEstimation[]>> GetPositionAdlQuantileEstimationAsync(string? symbol = null, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Get user fee rates
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/account%26trades/#user-commission-rate-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/commissionRate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol, for example `ETHUSDT`</param>
        /// <param name="receiveWindow">["<c>recvWindow</c>"] The receive window for which this request is active. When the request takes longer than this to complete the server will reject the request</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterUserCommission>> GetUserCommissionRateAsync(string symbol, long? receiveWindow = null, CancellationToken ct = default);

        /// <summary>
        /// Start a user stream. The resulting listen key can be used to subscribe to the user stream using the socket client. The stream will close after 60 minutes unless <see cref="KeepAliveUserStreamAsync">KeepAliveUserStreamAsync</see> is called.
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/user-data-streams/#start-user-data-stream-user_stream" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/listenKey
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult<string>> StartUserStreamAsync(CancellationToken ct = default);

        /// <summary>
        /// Keep alive the user stream. This should be called every 30 minutes to prevent the user stream being stopped
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/user-data-streams/#keepalive-user-data-stream-user_stream" /><br />
        /// Endpoint:<br />
        /// PUT /fapi/v3/listenKey
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to keep alive</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> KeepAliveUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Stop the user stream, no updates will be send anymore
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/user-data-streams/#close-user-data-stream-user_stream" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v3/listenKey
        /// </para>
        /// </summary>
        /// <param name="listenKey">["<c>listenKey</c>"] The listen key to stop</param>
        /// <param name="ct">Cancellation token</param>
        /// <returns></returns>
        Task<WebCallResult> StopUserStreamAsync(string listenKey, CancellationToken ct = default);

        /// <summary>
        /// Approve a builder to charge a fee, uses the parameters from the client options
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#approve-builder-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/approveBuilder
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ApproveBuilderAsync(CancellationToken ct = default);

        /// <summary>
        /// Approve a builder to charge a fee
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#approve-builder-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/approveBuilder
        /// </para>
        /// </summary>
        /// <param name="builderAddress">["<c>builder</c>"] The builder address</param>
        /// <param name="maxFeeRate">["<c>maxFeeRate</c>"] Max fee rate in percentage the builder can charge. 0.001 = 0.1%</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> ApproveBuilderAsync(string builderAddress, decimal maxFeeRate, CancellationToken ct = default);

        /// <summary>
        /// Update a previously approved builder
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#update-builder-trade" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/updateBuilder
        /// </para>
        /// </summary>
        /// <param name="builderAddress">["<c>builder</c>"] The builder address</param>
        /// <param name="newMaxFeeRate">["<c>maxFeeRate</c>"] Max new fee rate the builder can charge. 0.001 = 0.1%</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> UpdateBuilderAsync(string builderAddress, decimal newMaxFeeRate, CancellationToken ct = default);

        /// <summary>
        /// Remove a previously approved builder
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#delete-builder-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v3/builder
        /// </para>
        /// </summary>
        /// <param name="builderAddress">["<c>builder</c>"] The builder address</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> DeleteBuilderAsync(string builderAddress, CancellationToken ct = default);

        /// <summary>
        /// Get approved builders
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#get-builders-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/builder
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBuilder[]>> GetApprovedBuildersAsync(CancellationToken ct = default);

        /// <summary>
        /// Create or approve an agent
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#create-approve-agent-trade" /><br />
        /// Endpoint:<br />
        /// POST  /fapi/v3/approveAgent
        /// </para>
        /// </summary>
        /// <param name="agentName">["<c>agentName</c>"] Agent name</param>
        /// <param name="agentAddress">["<c>agentAddress</c>"] The agent address</param>
        /// <param name="expireTime">["<c>expired</c>"] Expire timestamp</param>
        /// <param name="ipWhitelist">["<c>ipWhitelist</c>"] IP whitelist</param>
        /// <param name="canSpotTrade">["<c>canSpotTrade</c>"] Spot trade permission</param>
        /// <param name="canPerpTrade">["<c>canPerpTrade</c>"] Perp trade permission</param>
        /// <param name="canWithdraw">["<c>canWithdraw</c>"] Withdraw permission</param>
        /// <param name="builder">["<c>builder</c>"] Builder address</param>
        /// <param name="builderMaxFeeRate">["<c>maxFeeRate</c>"] Builder max fee rate</param>
        /// <param name="builderName">["<c>builderName</c>"] Builder name</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> CreateOrApproveAgentAsync(
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
            CancellationToken ct = default);

        /// <summary>
        /// Update an agent
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#update-agent-trade" /><br />
        /// Endpoint:<br />
        /// POST  /fapi/v3/updateAgent
        /// </para>
        /// </summary>
        /// <param name="agentAddress">["<c>agentAddress</c>"] The agent address</param>
        /// <param name="ipWhitelist">["<c>ipWhitelist</c>"] IP whitelist</param>
        /// <param name="canSpotTrade">["<c>canSpotTrade</c>"] Spot trade permission</param>
        /// <param name="canPerpTrade">["<c>canPerpTrade</c>"] Perp trade permission</param>
        /// <param name="canWithdraw">["<c>canWithdraw</c>"] Withdraw permission</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> UpdateAgentAsync(
            string agentAddress,
            string? ipWhitelist,
            bool canSpotTrade,
            bool canPerpTrade,
            bool canWithdraw,
            CancellationToken ct = default);

        /// <summary>
        /// Delete an agent
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#delete-agent-trade" /><br />
        /// Endpoint:<br />
        /// DELETE /fapi/v3/agent
        /// </para>
        /// </summary>
        /// <param name="agentAddress">["<c>agentAddress</c>"] The agent address</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult> DeleteAgentAsync(
            string agentAddress,
            CancellationToken ct = default);

        /// <summary>
        /// Get list of agents
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/asterCode/endpoints/#get-agents-user_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/agent
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAgent[]>> GetAgentsAsync(CancellationToken ct = default);

        /// <summary>
        /// Get withdrawal info
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/deposit%26withdrawal/#get-user-withdraw-infov3" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/aster/user-withdraw-info<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterWithdrawInfo>> GetWithdrawInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get deposit and withdraw history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/deposit%26withdrawal/#get-deposit-and-withdraw-historyv3" /><br />
        /// Endpoint:<br />
        /// POST /fapi/v3/aster/deposit-withdraw-history<br />
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterDepositWithdrawal[]>> GetDepositWithdrawHistoryAsync(CancellationToken ct = default);

    }
}
