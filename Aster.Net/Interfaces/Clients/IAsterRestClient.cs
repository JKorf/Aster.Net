using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects.Options;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Aster Rest API. 
    /// </summary>
    public interface IAsterRestClient : IRestClient<AsterCredentials>
    {
        /// <summary>
        /// Spot V1 API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientSpotApi"/>
        public IAsterRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Spot V3 API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientSpotV3Api"/>
        public IAsterRestClientSpotV3Api SpotV3Api { get; }
        /// <summary>
        /// Futures V1 API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientFuturesApi"/>
        public IAsterRestClientFuturesApi FuturesApi { get; }
        /// <summary>
        /// Futures V3 API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientFuturesV3Api"/>
        public IAsterRestClientFuturesV3Api FuturesV3Api { get; }
    }
}
