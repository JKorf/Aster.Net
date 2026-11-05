using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.Interfaces.Clients;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Aster Rest API. 
    /// </summary>
    public interface IAsterRestClient : IRestClient<AsterCredentials>
    {
        /// <summary>
        /// Spot V3 API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientSpotV3Api"/>
        public IAsterRestClientSpotV3Api SpotV3Api { get; }
        /// <summary>
        /// Futures V3 API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientFuturesV3Api"/>
        public IAsterRestClientFuturesV3Api FuturesV3Api { get; }
    }
}
