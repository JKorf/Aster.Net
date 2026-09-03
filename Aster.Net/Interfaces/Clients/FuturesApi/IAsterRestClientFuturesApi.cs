using CryptoExchange.Net.Interfaces.Clients;
using System;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Aster Futures API endpoints
    /// </summary>
    public interface IAsterRestClientFuturesApi : IRestApiClient<AsterCredentials>, IDisposable
    {
        /// <summary>
        /// Endpoints related to account settings, info or actions
        /// </summary>
        /// <see cref="IAsterRestClientFuturesApiAccount" />
        public IAsterRestClientFuturesApiAccount Account { get; }

        /// <summary>
        /// Endpoints related to retrieving market and system data
        /// </summary>
        /// <see cref="IAsterRestClientFuturesApiExchangeData" />
        public IAsterRestClientFuturesApiExchangeData ExchangeData { get; }

        /// <summary>
        /// Endpoints related to orders and trades
        /// </summary>
        /// <see cref="IAsterRestClientFuturesApiTrading" />
        public IAsterRestClientFuturesApiTrading Trading { get; }

        /// <summary>
        /// [V1] Get the shared rest requests client. For new implementations prefer using <see cref="SharedApi"/>
        /// </summary>
        public IAsterRestClientFuturesApiShared SharedClient { get; }
        /// <summary>
        /// [V2] Gets the aggregate Shared API interface. Shared APIs provide a common,
        /// exchange-independent contract for accessing functionality across different
        /// exchange client libraries.
        /// </summary>
        public IAsterRestClientFuturesSharedApi SharedApi { get; }
    }
}
