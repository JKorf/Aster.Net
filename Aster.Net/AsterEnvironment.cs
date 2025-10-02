using CryptoExchange.Net.Objects;
using Aster.Net.Objects;

namespace Aster.Net
{
    /// <summary>
    /// Aster environments
    /// </summary>
    public class AsterEnvironment : TradeEnvironment
    {
        /// <summary>
        /// Futures Rest API address
        /// </summary>
        public string FuturesRestClientAddress { get; }

        /// <summary>
        /// Futures Socket API address
        /// </summary>
        public string FuturesSocketClientAddress { get; }

        /// <summary>
        /// Spot Rest API address
        /// </summary>
        public string SpotRestClientAddress { get; }

        /// <summary>
        /// Spot Socket API address
        /// </summary>
        public string SpotSocketClientAddress { get; }

        internal AsterEnvironment(
            string name,
            string spotRestAddress,
            string spotStreamAddress,
            string futuresRestAddress,
            string futuresStreamAddress) :
            base(name)
        {
            FuturesRestClientAddress = futuresRestAddress;
            FuturesSocketClientAddress = futuresStreamAddress;
            SpotRestClientAddress = spotRestAddress;
            SpotSocketClientAddress = spotStreamAddress;
        }

        /// <summary>
        /// ctor for DI, use <see cref="CreateCustom"/> for creating a custom environment
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public AsterEnvironment() : base(TradeEnvironmentNames.Live)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        { }

        /// <summary>
        /// Get the Aster environment by name
        /// </summary>
        public static AsterEnvironment? GetEnvironmentByName(string? name)
         => name switch
         {
             TradeEnvironmentNames.Live => Live,
             "" => Live,
             null => Live,
             _ => default
         };

        /// <summary>
        /// Available environment names
        /// </summary>
        /// <returns></returns>
        public static string[] All => [Live.Name];

        /// <summary>
        /// Live environment
        /// </summary>
        public static AsterEnvironment Live { get; }
            = new AsterEnvironment(TradeEnvironmentNames.Live,
                                     AsterApiAddresses.Default.SpotRestClientAddress,
                                     AsterApiAddresses.Default.SpotSocketClientAddress,
                                     AsterApiAddresses.Default.FuturesRestClientAddress,
                                     AsterApiAddresses.Default.FuturesSocketClientAddress);

        /// <summary>
        /// Create a custom environment
        /// </summary>
        public static AsterEnvironment CreateCustom(
                        string name,
                        string spotRestAddress,
                        string spotSocketStreamsAddress,
                        string futuresRestAddress,
                        string futuresSocketAddress)
            => new AsterEnvironment(name, spotRestAddress, spotSocketStreamsAddress, futuresRestAddress, futuresSocketAddress);
    }
}
