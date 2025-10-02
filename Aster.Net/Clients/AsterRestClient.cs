using Microsoft.Extensions.Logging;
using System.Net.Http;
using System;
using CryptoExchange.Net.Authentication;
using Aster.Net.Interfaces.Clients;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Options;
using CryptoExchange.Net.Objects.Options;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Clients.FuturesApi;
using Aster.Net.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotApi;

namespace Aster.Net.Clients
{
    /// <inheritdoc cref="IAsterRestClient" />
    public class AsterRestClient : BaseRestClient, IAsterRestClient
    {
        #region Api clients

        /// <inheritdoc />
        public IAsterRestClientSpotApi SpotApi { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesApi FuturesApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of the AsterRestClient using provided options
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public AsterRestClient(Action<AsterRestOptions>? optionsDelegate = null)
            : this(null, null, Options.Create(ApplyOptionsDelegate(optionsDelegate)))
        {
        }

        /// <summary>
        /// Create a new instance of the AsterRestClient using provided options
        /// </summary>
        /// <param name="options">Option configuration</param>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="httpClient">Http client for this client</param>
        public AsterRestClient(HttpClient? httpClient, ILoggerFactory? loggerFactory, IOptions<AsterRestOptions> options) : base(loggerFactory, "Aster")
        {
            Initialize(options.Value);
            
            SpotApi = AddApiClient(new AsterRestClientSpotApi(_logger, httpClient, options.Value));
            FuturesApi = AddApiClient(new AsterRestClientFuturesApi(_logger, httpClient, options.Value));
        }

        #endregion

        /// <inheritdoc />
        public void SetOptions(UpdateOptions options)
        {
            SpotApi.SetOptions(options);
            FuturesApi.SetOptions(options);
        }

        /// <summary>
        /// Set the default options to be used when creating new clients
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public static void SetDefaultOptions(Action<AsterRestOptions> optionsDelegate)
        {
            AsterRestOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {   
            SpotApi.SetApiCredentials(credentials);
            FuturesApi.SetApiCredentials(credentials);
        }
    }
}
