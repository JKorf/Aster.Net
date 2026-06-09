using Aster.Net.Clients;
using Aster.Net.Clients.FuturesV3Api;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Aster.Net.Utils
{
    /// <summary>
    /// Util methods for the Aster API
    /// </summary>
    public static class AsterUtils
    {
        private static readonly SemaphoreSlim _semaphoreBuilderFee = new SemaphoreSlim(1, 1);
        private static bool _checkedBuilderFee = false;
        // We should only send builder credentials if the check has succeeded
        internal static bool _builderFeeSuccess = false;

        internal static async Task<CallResult> CheckBuilderFeeAsync(AsterRestClient client)
        {
            var futuresV3Api = (AsterRestClientFuturesV3Api)client.FuturesV3Api;
            if (futuresV3Api.AuthenticationProvider?.ApiCredentials.V3?.PrivateKey == null)
                // No (V3) credentials provided, no need to check builder fee
                return CallResult.Ok();

            var envName = client.ClientOptions.Environment.Name;
            if (!envName.Equals(TradeEnvironmentNames.Live, StringComparison.Ordinal))
                return CallResult.Ok();

            var options = client.ClientOptions;
            if(_checkedBuilderFee)
                return CallResult.Ok();

            if (options.BuilderFeePercentage == null
                || options.BuilderFeePercentage == 0)
            {
                // No builder fee, no need to check
                return CallResult.Ok();
            }

            await _semaphoreBuilderFee.WaitAsync().ConfigureAwait(false);
            try
            {
                // Set to true even if the check fails to avoid continuously trying to check and approve the builder fee if there's an issue
                _checkedBuilderFee = true;

                var approvedBuildersResult = await client.FuturesV3Api.Account.GetApprovedBuildersAsync().ConfigureAwait(false);
                if (!approvedBuildersResult.Success)
                    return CallResult.Fail(approvedBuildersResult.Error);

                var builder = approvedBuildersResult.Data.SingleOrDefault(x => x.BuilderAddress.Equals(options.BuilderAddress, StringComparison.OrdinalIgnoreCase));
                var targetBps = options.BuilderFeePercentage.Value / 100;
                if (builder != null && builder.MaxFeeRate >= targetBps)
                {
                    // Builder fee is approved, we're good
                    _builderFeeSuccess = true;
                    return CallResult.Ok();
                }

                var approveResult = await client.FuturesV3Api.Account.ApproveBuilderAsync().ConfigureAwait(false);
                if (approveResult.Success)
                    _builderFeeSuccess = true;

                return CallResult.Ok();
            }
            finally
            {
                _semaphoreBuilderFee.Release();
            }
        }
    }
}
