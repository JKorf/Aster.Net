using Aster.Net.Clients;
using Aster.Net.Clients.FuturesV3Api;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Utils
{
    /// <summary>
    /// Util methods for the Aster API
    /// </summary>
    public static class AsterUtils
    {
        internal static readonly ConcurrentDictionary<string, BuilderFeeStatus> _builderFeeStatus = new ConcurrentDictionary<string, BuilderFeeStatus>();

        internal static async Task<CallResult> CheckBuilderFeeAsync(AsterRestClient client)
        {
            var futuresV3Api = (AsterRestClientFuturesV3Api)client.FuturesV3Api;
            if (futuresV3Api.AuthenticationProvider?.ApiCredentials.V3?.PrivateKey == null)
                // No (V3) credentials provided, no need to check builder fee
                return CallResult.Ok();

            var key = futuresV3Api.AuthenticationProvider!.ApiCredentials.V3.Key;

            var envName = client.ClientOptions.Environment.Name;
            if (!envName.Equals(TradeEnvironmentNames.Live, StringComparison.Ordinal))
                return CallResult.Ok();

            var options = client.ClientOptions;
            var builderStatus = _builderFeeStatus.GetOrAdd(key, (key) => new BuilderFeeStatus());
            if (builderStatus.Checked)
                return CallResult.Ok();

            if (options.BuilderFeePercentage == null
                || options.BuilderFeePercentage == 0)
            {
                // No builder fee, no need to check
                return CallResult.Ok();
            }

            await builderStatus.Semaphore.WaitAsync().ConfigureAwait(false);
            try
            {
                // Set to true even if the check fails to avoid continuously trying to check and approve the builder fee if there's an issue
                builderStatus.Checked = true;

                var approvedBuildersResult = await client.FuturesV3Api.Account.GetApprovedBuildersAsync().ConfigureAwait(false);
                if (!approvedBuildersResult.Success)
                    return CallResult.Fail(approvedBuildersResult.Error);

                var builder = approvedBuildersResult.Data.SingleOrDefault(x => x.BuilderAddress.Equals(options.BuilderAddress, StringComparison.OrdinalIgnoreCase));
                var targetBps = options.BuilderFeePercentage.Value / 100;
                if (builder != null && builder.MaxFeeRate >= targetBps)
                {
                    // Builder fee already approved, we're good
                    builderStatus.Success = true;
                    return CallResult.Ok();
                }

                var approveResult = await client.FuturesV3Api.Account.ApproveBuilderAsync().ConfigureAwait(false);
                if (approveResult.Success)
                    builderStatus.Success = true;

                return CallResult.Ok();
            }
            finally
            {
                builderStatus.Semaphore.Release();
            }
        }
    }

    internal class BuilderFeeStatus
    {
        /// <summary>
        /// Whether builder fee was checked
        /// </summary>
        public bool Checked { get; set; }
        /// <summary>
        /// Whether builder fee is approved and can be applied
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Key-specific semaphore
        /// </summary>
        public SemaphoreSlim Semaphore { get; set; } = new SemaphoreSlim(1, 1);
    }
}
