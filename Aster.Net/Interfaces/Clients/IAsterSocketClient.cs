using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Aster websocket API
    /// </summary>
    public interface IAsterSocketClient : ISocketClient<AsterCredentials>
    {
        /// <summary>
        /// Spot V1 API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientSpotApi"/>
        public IAsterSocketClientSpotApi SpotApi { get; }
        /// <summary>
        /// Spot V3 API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientSpotApi"/>
        public IAsterSocketClientSpotV3Api SpotV3Api { get; }

        /// <summary>
        /// Futures V1 API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientFuturesApi"/>
        public IAsterSocketClientFuturesApi FuturesApi { get; }

        /// <summary>
        /// Futures V3 API endpoints
        /// </summary>
        /// <see cref="IAsterSocketClientFuturesV3Api"/>
        public IAsterSocketClientFuturesV3Api FuturesV3Api { get; }
    }
}
