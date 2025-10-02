using CryptoExchange.Net.Objects.Options;
using System;

namespace Aster.Net.Objects.Options
{
    /// <summary>
    /// Options for the AsterRestClient
    /// </summary>
    public class AsterRestOptions : RestExchangeOptions<AsterEnvironment>
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
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
