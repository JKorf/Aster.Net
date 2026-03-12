using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Objects;
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
        /// Spot API endpoints
        /// </summary>
        /// <see cref="IAsterRestClientSpotApi"/>
        public IAsterRestClientSpotApi SpotApi { get; }
        /// <summary>
        /// Futures API endpoints
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
