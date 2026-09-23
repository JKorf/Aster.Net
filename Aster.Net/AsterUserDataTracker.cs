using Aster.Net.Interfaces.Clients;
using CryptoExchange.Net.Trackers.UserData;
using CryptoExchange.Net.Trackers.UserData.Objects;
using Microsoft.Extensions.Logging;

namespace Aster.Net
{
    /// <inheritdoc />
    public class AsterUserSpotDataTracker : UserSpotDataTracker
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AsterUserSpotDataTracker(
            ILogger<AsterUserSpotDataTracker> logger,
            IAsterRestClient restClient,
            IAsterSocketClient socketClient,
            string? userIdentifier,
            SpotUserDataTrackerConfig? config) : base(
                logger,
                restClient.SpotV3Api.SharedApi,
                restClient.SpotV3Api.SharedApi,
                socketClient.SpotV3Api.SharedApi,

                restClient.SpotV3Api.SharedApi,
                restClient.SpotV3Api.SharedApi,
                socketClient.SpotV3Api.SharedApi,

                restClient.SpotV3Api.SharedApi,
                null,
                userIdentifier,
                config ?? new SpotUserDataTrackerConfig())
        {

        }
    }

    /// <inheritdoc />
    public class AsterUserFuturesDataTracker : UserFuturesDataTracker
    {
        /// <inheritdoc />
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterUserFuturesDataTracker(
            ILogger<AsterUserFuturesDataTracker> logger,
            IAsterRestClient restClient,
            IAsterSocketClient socketClient,
            string? userIdentifier,
            FuturesUserDataTrackerConfig? config) : base(logger,
                restClient.FuturesV3Api.SharedApi,
                restClient.FuturesV3Api.SharedApi,
                socketClient.FuturesV3Api.SharedApi,

                restClient.FuturesV3Api.SharedApi,
                restClient.FuturesV3Api.SharedApi,
                socketClient.FuturesV3Api.SharedApi,

                restClient.FuturesV3Api.SharedApi,
                null,

                restClient.FuturesV3Api.SharedApi,
                socketClient.FuturesV3Api.SharedApi,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {

        }
    }
}
