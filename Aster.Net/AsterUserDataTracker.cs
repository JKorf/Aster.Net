using Aster.Net.Interfaces.Clients;
using CryptoExchange.Net.Trackers.UserData;
using Microsoft.Extensions.Logging;

namespace Aster.Net
{
    public class AsterUserSpotDataTracker : UserSpotDataTracker
    {
        public AsterUserSpotDataTracker(
            ILogger<AsterUserSpotDataTracker> logger,
            IAsterRestClient restClient,
            IAsterSocketClient socketClient,
            string? userIdentifier,
            UserDataTrackerConfig config) : base(logger, restClient.SpotApi.SharedClient, socketClient.SpotApi.SharedClient, userIdentifier, config)
        {

        }
    }

    public class AsterUserFuturesDataTracker : UserFuturesDataTracker
    {
        protected override bool WebsocketPositionUpdatesAreFullSnapshots => false;

        public AsterUserFuturesDataTracker(
            ILogger<AsterUserFuturesDataTracker> logger,
            IAsterRestClient restClient,
            IAsterSocketClient socketClient,
            string? userIdentifier,
            UserDataTrackerConfig config) : base(logger, restClient.FuturesApi.SharedClient, socketClient.FuturesApi.SharedClient, userIdentifier, config)
        {

        }
    }
}
