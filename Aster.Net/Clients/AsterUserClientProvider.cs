using Aster.Net.Interfaces.Clients;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Aster.Net.Clients
{
    /// <inheritdoc />
    public class AsterUserClientProvider : IAsterUserClientProvider
    {
        private static ConcurrentDictionary<string, IAsterRestClient> _restClients = new ConcurrentDictionary<string, IAsterRestClient>();
        private static ConcurrentDictionary<string, IAsterSocketClient> _socketClients = new ConcurrentDictionary<string, IAsterSocketClient>();
        
        private readonly IOptions<AsterRestOptions> _restOptions;
        private readonly IOptions<AsterSocketOptions> _socketOptions;
        private readonly HttpClient _httpClient;
        private readonly ILoggerFactory? _loggerFactory;

        /// <inheritdoc />
        public string ExchangeName => AsterExchange.ExchangeName;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="optionsDelegate">Options to use for created clients</param>
        public AsterUserClientProvider(Action<AsterOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate).Rest), Options.Create(ApplyOptionsDelegate(optionsDelegate).Socket))
        {
        }
        
        /// <summary>
        /// ctor
        /// </summary>
        public AsterUserClientProvider(
            HttpClient? httpClient,
            ILoggerFactory? loggerFactory,
            IOptions<AsterRestOptions> restOptions,
            IOptions<AsterSocketOptions> socketOptions)
        {
            _httpClient = httpClient ?? new HttpClient();
            _loggerFactory = loggerFactory;
            _restOptions = restOptions;
            _socketOptions = socketOptions;
        }

        /// <inheritdoc />
        public void InitializeUserClient(string userIdentifier, ApiCredentials credentials, AsterEnvironment? environment = null)
        {
            CreateRestClient(userIdentifier, credentials, environment);
            CreateSocketClient(userIdentifier, credentials, environment);
        }

        /// <inheritdoc />
        public void ClearUserClients(string userIdentifier)
        {
            _restClients.TryRemove(userIdentifier, out _);
            _socketClients.TryRemove(userIdentifier, out _);
        }

        /// <inheritdoc />
        public IAsterRestClient GetRestClient(string userIdentifier, ApiCredentials? credentials = null, AsterEnvironment? environment = null)
        {
            if (!_restClients.TryGetValue(userIdentifier, out var client))
                client = CreateRestClient(userIdentifier, credentials, environment);

            return client;
        }

        /// <inheritdoc />
        public IAsterSocketClient GetSocketClient(string userIdentifier, ApiCredentials? credentials = null, AsterEnvironment? environment = null)
        {
            if (!_socketClients.TryGetValue(userIdentifier, out var client))
                client = CreateSocketClient(userIdentifier, credentials, environment);

            return client;
        }

        private IAsterRestClient CreateRestClient(string userIdentifier, ApiCredentials? credentials, AsterEnvironment? environment)
        {
            var clientRestOptions = SetRestEnvironment(environment);
            var client = new AsterRestClient(_httpClient, _loggerFactory, clientRestOptions);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _restClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IAsterSocketClient CreateSocketClient(string userIdentifier, ApiCredentials? credentials, AsterEnvironment? environment)
        {
            var clientSocketOptions = SetSocketEnvironment(environment);
            var client = new AsterSocketClient(clientSocketOptions!, _loggerFactory);
            if (credentials != null)
            {
                client.SetApiCredentials(credentials);
                _socketClients.TryAdd(userIdentifier, client);
            }
            return client;
        }

        private IOptions<AsterRestOptions> SetRestEnvironment(AsterEnvironment? environment)
        {
            if (environment == null)
                return _restOptions;

            var newRestClientOptions = new AsterRestOptions();
            var restOptions = _restOptions.Value.Set(newRestClientOptions);
            newRestClientOptions.Environment = environment;
            return Options.Create(newRestClientOptions);
        }

        private IOptions<AsterSocketOptions> SetSocketEnvironment(AsterEnvironment? environment)
        {
            if (environment == null)
                return _socketOptions;

            var newSocketClientOptions = new AsterSocketOptions();
            var restOptions = _socketOptions.Value.Set(newSocketClientOptions);
            newSocketClientOptions.Environment = environment;
            return Options.Create(newSocketClientOptions);
        }

        private static T ApplyOptionsDelegate<T>(Action<T>? del) where T : new()
        {
            var opts = new T();
            del?.Invoke(opts);
            return opts;
        }
    }
}
