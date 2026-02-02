using Aster.Net.Clients;
using Aster.Net.Interfaces;
using Aster.Net.Interfaces.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Trackers.Klines;
using CryptoExchange.Net.Trackers.Trades;
using CryptoExchange.Net.Trackers.UserData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;

namespace Aster.Net
{
    /// <inheritdoc />
    public class AsterTrackerFactory : IAsterTrackerFactory
    {
        private readonly IServiceProvider? _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        public AsterTrackerFactory()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider">Service provider for resolving logging and clients</param>
        public AsterTrackerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc />
        public bool CanCreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval)
        {
            var client = _serviceProvider?.GetRequiredService<IAsterSocketClient>() ?? new AsterSocketClient();
            SubscribeKlineOptions klineOptions = symbol.TradingMode == TradingMode.Spot ? client.SpotApi.SharedClient.SubscribeKlineOptions : client.FuturesApi.SharedClient.SubscribeKlineOptions;
            return klineOptions.IsSupported(interval); 
        }

        /// <inheritdoc />
        public bool CanCreateTradeTracker(SharedSymbol symbol) => true;

        /// <inheritdoc />
        public IKlineTracker CreateKlineTracker(SharedSymbol symbol, SharedKlineInterval interval, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IAsterRestClient>() ?? new AsterRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IAsterSocketClient>() ?? new AsterSocketClient();

            IKlineRestClient sharedRestClient;
            IKlineSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.FuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }

            return new KlineTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                sharedSocketClient,
                symbol,
                interval,
                limit,
                period
                );
        }

        /// <inheritdoc />
        public ITradeTracker CreateTradeTracker(SharedSymbol symbol, int? limit = null, TimeSpan? period = null)
        {
            var restClient = _serviceProvider?.GetRequiredService<IAsterRestClient>() ?? new AsterRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IAsterSocketClient>() ?? new AsterSocketClient();

            IRecentTradeRestClient? sharedRestClient;
            ITradeSocketClient sharedSocketClient;
            if (symbol.TradingMode == TradingMode.Spot)
            {
                sharedRestClient = restClient.SpotApi.SharedClient;
                sharedSocketClient = socketClient.SpotApi.SharedClient;
            }
            else
            {
                sharedRestClient = restClient.FuturesApi.SharedClient;
                sharedSocketClient = socketClient.FuturesApi.SharedClient;
            }

            return new TradeTracker(
                _serviceProvider?.GetRequiredService<ILoggerFactory>().CreateLogger(restClient.Exchange),
                sharedRestClient,
                null,
                sharedSocketClient,
                symbol,
                limit,
                period
                );
        }

        public IUserSpotDataTracker CreateUserSpotDataTracker(UserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IAsterRestClient>() ?? new AsterRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IAsterSocketClient>() ?? new AsterSocketClient();
            return new AsterUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<AsterUserSpotDataTracker>>() ?? new NullLogger<AsterUserSpotDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserSpotDataTracker CreateUserSpotDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, AsterEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IAsterUserClientProvider>() ?? new AsterUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new AsterUserSpotDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<AsterUserSpotDataTracker>>() ?? new NullLogger<AsterUserSpotDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }

        public IUserFuturesDataTracker CreateUserFuturesDataTracker(UserDataTrackerConfig config)
        {
            var restClient = _serviceProvider?.GetRequiredService<IAsterRestClient>() ?? new AsterRestClient();
            var socketClient = _serviceProvider?.GetRequiredService<IAsterSocketClient>() ?? new AsterSocketClient();
            return new AsterUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<AsterUserFuturesDataTracker>>() ?? new NullLogger<AsterUserFuturesDataTracker>(),
                restClient,
                socketClient,
                null,
                config
                );
        }

        /// <inheritdoc />
        public IUserFuturesDataTracker CreateUserFuturesDataTracker(string userIdentifier, UserDataTrackerConfig config, ApiCredentials credentials, AsterEnvironment? environment = null)
        {
            var clientProvider = _serviceProvider?.GetRequiredService<IAsterUserClientProvider>() ?? new AsterUserClientProvider();
            var restClient = clientProvider.GetRestClient(userIdentifier, credentials, environment);
            var socketClient = clientProvider.GetSocketClient(userIdentifier, credentials, environment);
            return new AsterUserFuturesDataTracker(
                _serviceProvider?.GetRequiredService<ILogger<AsterUserFuturesDataTracker>>() ?? new NullLogger<AsterUserFuturesDataTracker>(),
                restClient,
                socketClient,
                userIdentifier,
                config
                );
        }
    }
}
