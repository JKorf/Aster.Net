using Aster.Net.Clients.MessageHandlers;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Objects.Options;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Converters.MessageParsing.DynamicConverters;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesApi
{
    /// <inheritdoc cref="IAsterRestClientFuturesApi" />
    internal partial class AsterRestClientFuturesApi : RestApiClient<AsterEnvironment, AsterV1AuthenticationProvider, AsterCredentials>, IAsterRestClientFuturesApi
    {
        #region fields 
        protected override IRestMessageHandler MessageHandler { get; } = new AsterRestMessageHandler(AsterErrors.FuturesErrors);
        protected override ErrorMapping ErrorMapping => AsterErrors.FuturesErrors;

        public new AsterRestOptions ClientOptions => (AsterRestOptions)base.ClientOptions;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IAsterRestClientFuturesApiAccount Account { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Aster";
        #endregion

        #region constructor/destructor
        internal AsterRestClientFuturesApi(ILoggerFactory? loggerFactory, HttpClient? httpClient, AsterRestOptions options)
            : base(loggerFactory,
                  AsterExchange.Metadata.Id,
                  httpClient,
                  options.Environment.FuturesRestClientAddress,
                  options,
                  options.FuturesOptions)
        {
            Account = new AsterRestClientFuturesApiAccount(this);
            ExchangeData = new AsterRestClientFuturesApiExchangeData(_logger, this);
            Trading = new AsterRestClientFuturesApiTrading(_logger, this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(AsterExchange._serializerContext);

        /// <inheritdoc />
        protected override AsterV1AuthenticationProvider CreateAuthenticationProvider(AsterCredentials credentials)
            => new AsterV1AuthenticationProvider(credentials);

        internal async Task<HttpResult> SendAsync(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync<Unit>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            var result = await base.SendAsync<T>(definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result.Success && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        /// <inheritdoc />
        protected override Task<HttpResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => AsterExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IAsterRestClientFuturesApiShared SharedClient => this;

    }
}
