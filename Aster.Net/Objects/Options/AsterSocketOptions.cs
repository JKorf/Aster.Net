using CryptoExchange.Net.Objects.Options;

namespace Aster.Net.Objects.Options
{
    /// <summary>
    /// Options for the AsterSocketClient
    /// </summary>
    public class AsterSocketOptions : SocketExchangeOptions<AsterEnvironment>
    {
        /// <summary>
        /// Default options for new clients
        /// </summary>
        internal static AsterSocketOptions Default { get; set; } = new AsterSocketOptions()
        {
            Environment = AsterEnvironment.Live,
            SocketSubscriptionsCombineTarget = 10
        };

        /// <summary>
        /// ctor
        /// </summary>
        public AsterSocketOptions()
        {
            Default?.Set(this);
        }

        /// <summary>
        /// Spot API options
        /// </summary>
        public SocketApiOptions SpotOptions { get; private set; } = new SocketApiOptions();

        /// <summary>
        /// Futures API options
        /// </summary>
        public SocketApiOptions FuturesOptions { get; private set; } = new SocketApiOptions();


        internal AsterSocketOptions Set(AsterSocketOptions targetOptions)
        {
            targetOptions = base.Set<AsterSocketOptions>(targetOptions);            
            targetOptions.FuturesOptions = FuturesOptions.Set(targetOptions.FuturesOptions);
            targetOptions.SpotOptions = SpotOptions.Set(targetOptions.SpotOptions);
            return targetOptions;
        }
    }
}
