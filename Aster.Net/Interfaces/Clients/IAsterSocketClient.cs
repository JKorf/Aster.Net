using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Aster websocket API
    /// </summary>
    public interface IAsterSocketClient : ISocketClient
    {
        /// <summary>
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientSpotApi"/>
        public IAsterSocketClientSpotApi SpotApi { get; }

        /// <summary>
        /// Futures API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientFuturesApi"/>
        public IAsterSocketClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
