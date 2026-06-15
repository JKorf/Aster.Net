using Aster.Net.Interfaces.Clients;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace Aster.Net.Clients
{
    /// <inheritdoc />
    public class AsterUserClientProvider : UserClientProvider<
        IAsterRestClient,
        IAsterSocketClient,
        AsterRestOptions,
        AsterSocketOptions,
        AsterCredentials,
        AsterEnvironment
        >, IAsterUserClientProvider
    {
        /// <inheritdoc />
        public override string ExchangeName => AsterExchange.ExchangeName;

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
            : base(httpClient, loggerFactory, restOptions, socketOptions)
        {
        }

        /// <inheritdoc />
        protected override IAsterRestClient ConstructRestClient(HttpClient client, ILoggerFactory? loggerFactory, IOptions<AsterRestOptions> options)
            => new AsterRestClient(client, loggerFactory, options);
        /// <inheritdoc />
        protected override IAsterSocketClient ConstructSocketClient(ILoggerFactory? loggerFactory, IOptions<AsterSocketOptions> options)
            => new AsterSocketClient(options, loggerFactory);
    }
}
