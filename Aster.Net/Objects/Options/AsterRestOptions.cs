using CryptoExchange.Net.Objects.Options;
using System;

namespace Aster.Net.Objects.Options
{
    /// <summary>
    /// Options for the AsterRestClient
    /// </summary>
    public class AsterRestOptions : RestExchangeOptions<AsterEnvironment, AsterCredentials>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static AsterRestOptions Default { get; set; } = new AsterRestOptions()
        {
            Environment = AsterEnvironment.Live,
            AutoTimestamp = true
        };

        /// <summary>
        /// ctor
        /// </summary>
        public AsterRestOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// The builder fee percentage to apply to orders. This refers to a fee percentage being paid to the developer to support development. Defaults to 1bps/0.01%, but can be set to 0/null
        /// </summary>
        public decimal? BuilderFeePercentage { get; set; } = 0.01m;

        /// <summary>
        /// Address of the builder
        /// </summary>
        public string BuilderAddress { get; set; } = "0x64E807d36a59E28265167e1473E0DF83821Dc291";

        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiOptions SpotOptions { get; private set; } = new RestApiOptions();

        /// <summary>
        /// Futures API options
        /// </summary>
        public RestApiOptions FuturesOptions { get; private set; } = new RestApiOptions();

        internal AsterRestOptions Set(AsterRestOptions targetOptions)
        {
            targetOptions = base.Set<AsterRestOptions>(targetOptions);
            targetOptions.BuilderAddress = BuilderAddress;
            targetOptions.BuilderFeePercentage = BuilderFeePercentage;
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
