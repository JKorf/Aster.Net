namespace Aster.Net.Objects
{
    /// <summary>
    /// Api addresses
    /// </summary>
    public class AsterApiAddresses
    {
        /// <summary>
        /// The address used by the AsterRestClient for the Futures API
        /// </summary>
        public string FuturesRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the AsterSocketClient for the Futures websocket API
        /// </summary>
        public string FuturesSocketClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the AsterRestClient for the Spot API
        /// </summary>
        public string SpotRestClientAddress { get; set; } = "";
        /// <summary>
        /// The address used by the AsterSocketClient for the Spot websocket API
        /// </summary>
        public string SpotSocketClientAddress { get; set; } = "";

        /// <summary>
        /// The default addresses to connect to the Aster API
        /// </summary>
        public static AsterApiAddresses Default = new AsterApiAddresses
        {
            FuturesRestClientAddress = "https://fapi.asterdex.com",
            FuturesSocketClientAddress = "wss://fstream.asterdex.com",
            SpotRestClientAddress = "https://sapi.asterdex.com",
            SpotSocketClientAddress = "wss://sstream.asterdex.com"
        };
    }
}
