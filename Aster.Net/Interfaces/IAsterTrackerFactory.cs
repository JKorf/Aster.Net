using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Trackers.UserData;

namespace Aster.Net.Interfaces
{
    /// <summary>
    /// Tracker factory
    /// </summary>
    public interface IAsterTrackerFactory : ITrackerFactory
    {
        IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, AsterEnvironment? environment = null);
        IUserSpotDataTracker CreateUserSpotDataTracker(UserDataTrackerConfig config);
        IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, AsterEnvironment? environment = null);
        IUserFuturesDataTracker CreateUserFuturesDataTracker(UserDataTrackerConfig config);
    }
}
