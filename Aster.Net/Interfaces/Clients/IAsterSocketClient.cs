using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.Interfaces.Clients;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Aster websocket API
    /// </summary>
    public interface IAsterSocketClient : ISocketClient<AsterCredentials>
    {
        /// <summary>
        /// Spot V3 API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientSpotV3Api"/>
        public IAsterSocketClientSpotV3Api SpotV3Api { get; }

        /// <summary>
        /// Futures V3 API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientFuturesV3Api"/>
        public IAsterSocketClientFuturesV3Api FuturesV3Api { get; }
    }
}
