using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Aster.Net.Interfaces.Clients.FuturesV3Api
{
    /// <summary>
    /// Aster Futures API endpoints
    /// </summary>
    public interface IAsterRestClientFuturesV3Api : IRestApiClient<AsterCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IAsterRestClientFuturesV3ApiAccount" />
        public IAsterRestClientFuturesV3ApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IAsterRestClientFuturesV3ApiExchangeData" />
        public IAsterRestClientFuturesV3ApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IAsterRestClientFuturesV3ApiTrading" />
        public IAsterRestClientFuturesV3ApiTrading Trading { get; }

        /// <summary>
        /// Get the shared rest requests client. This interface is shared with other exchanges to allow for a common implementation for different exchanges.
        /// </summary>
        public IAsterRestClientFuturesV3ApiShared SharedClient { get; }

    }
}
