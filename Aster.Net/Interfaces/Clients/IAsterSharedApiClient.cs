using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotV3Api;

namespace Aster.Net.Interfaces.Clients
{
    /// <summary>
    /// Client for the shared REST and WebSocket API implementations of Aster
    /// </summary>
    public interface IAsterSharedApiClient
    {
        /// <summary>
        /// Spot V1 REST shared API implementations
        /// </summary>
        IAsterRestClientSpotSharedApi SpotRest { get; }

        /// <summary>
        /// Futures V1 REST shared API implementations
        /// </summary>
        IAsterRestClientFuturesSharedApi FuturesRest { get; }

        /// <summary>
        /// Spot V3 REST shared API implementations
        /// </summary>
        IAsterRestClientSpotV3SharedApi SpotV3Rest { get; }

        /// <summary>
        /// Futures V3 REST shared API implementations
        /// </summary>
        IAsterRestClientFuturesV3SharedApi FuturesV3Rest { get; }

        /// <summary>
        /// Spot V1 WebSocket shared API implementations
        /// </summary>
        IAsterSocketClientSpotSharedApi SpotSocket { get; }

        /// <summary>
        /// Futures V1 WebSocket shared API implementations
        /// </summary>
        IAsterSocketClientFuturesSharedApi FuturesSocket { get; }

        /// <summary>
        /// Spot V3 WebSocket shared API implementations
        /// </summary>
        IAsterSocketClientSpotV3SharedApi SpotV3Socket { get; }

        /// <summary>
        /// Futures V3 WebSocket shared API implementations
        /// </summary>
        IAsterSocketClientFuturesV3SharedApi FuturesV3Socket { get; }
    }
}
