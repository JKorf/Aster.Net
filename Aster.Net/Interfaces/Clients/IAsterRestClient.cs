using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects.Options;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for accessing the Aster Rest API. 
    /// </summary>
    public interface IAsterRestClient : IRestClient
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
        /// Update specific options
        /// </summary>
        /// <param name="options">Options to update. Only specific options are changeable after the client has been created</param>
        void SetOptions(UpdateOptions options);

        /// <summary>
        /// Set the API credentials for this client. All Api clients in this client will use the new credentials, regardless of earlier set options.
        /// </summary>
        /// <param name="credentials">The credentials to set</param>
        void SetApiCredentials(ApiCredentials credentials);
    }
}
