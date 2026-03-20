using Aster.Net.Clients.SpotApi;
using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Aster.Net.Interfaces.Clients.SpotV3Api
{
    /// <summary>
    /// Aster Spot V3 API endpoints
    /// </summary>
    public interface IAsterRestClientSpotV3Api : IRestApiClient<AsterCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IAsterRestClientSpotV3ApiAccount" />
        public IAsterRestClientSpotV3ApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IAsterRestClientSpotV3ApiExchangeData" />
        public IAsterRestClientSpotV3ApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IAsterRestClientSpotV3ApiTrading" />
        public IAsterRestClientSpotV3ApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IAsterRestClientSpotV3ApiShared SharedClient { get; }
    }
}
