using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Aster Futures API endpoints
    /// </summary>
    public interface IAsterRestClientFuturesV3Api : IRestApiClient, IDisposable
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

    }
}
