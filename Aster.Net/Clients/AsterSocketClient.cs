using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Aster.Net.Interfaces.Clients;
using Aster.Net.Objects.Options;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Clients.FuturesApi;
using Aster.Net.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotApi;

namespace Aster.Net.Clients
{
    /// <inheritdoc cref="IAsterSocketClient" />
    public class AsterSocketClient : BaseSocketClient, IAsterSocketClient
    {
        #region fields
        #endregion

        #region Api clients
        
         /// <inheritdoc />
        public IAsterSocketClientFuturesApi FuturesApi { get; }
         /// <inheritdoc />
        public IAsterSocketClientSpotApi SpotApi { get; }

        #endregion

        #region constructor/destructor

        /// <summary>
        /// Create a new instance of AsterSocketClient
        /// </summary>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public AsterSocketClient(Action<AsterSocketOptions>? optionsDelegate = null)
            : this(Options.Create(ApplyOptionsDelegate(optionsDelegate)), null)
        {
        }

        /// <summary>
        /// Create a new instance of AsterSocketClient
        /// </summary>
        /// <param name="loggerFactory">The logger factory</param>
        /// <param name="options">Option configuration</param>
        public AsterSocketClient(IOptions<AsterSocketOptions> options, ILoggerFactory? loggerFactory = null) : base(loggerFactory, "Aster")
        {
            Initialize(options.Value);

            SpotApi = AddApiClient(new AsterSocketClientSpotApi(_logger, options.Value));
            FuturesApi = AddApiClient(new AsterSocketClientFuturesApi(_logger, options.Value));
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
        public static void SetDefaultOptions(Action<AsterSocketOptions> optionsDelegate)
        {
            AsterSocketOptions.Default = ApplyOptionsDelegate(optionsDelegate);
        }

        /// <inheritdoc />
        public void SetApiCredentials(ApiCredentials credentials)
        {
            SpotApi.SetApiCredentials(credentials);
            FuturesApi.SetApiCredentials(credentials);
        }
    }
}
