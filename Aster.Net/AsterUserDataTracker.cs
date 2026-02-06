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
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
                restClient.SpotApi.SharedClient,
                socketClient.SpotApi.SharedClient,
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
                restClient.FuturesApi.SharedClient,
                restClient.FuturesApi.SharedClient,
                restClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                restClient.FuturesApi.SharedClient,
                socketClient.FuturesApi.SharedClient,
                null,
                socketClient.FuturesApi.SharedClient,
                userIdentifier,
                config ?? new FuturesUserDataTrackerConfig())
        {

        }
    }
}
