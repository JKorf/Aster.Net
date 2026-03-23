using Aster.Net.Clients.MessageHandlers;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using Aster.Net.Objects.Options;
using Aster.Net.Utils;
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
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotV3Api
{
    /// <inheritdoc cref="IAsterRestClientSpotV3Api" />
    internal partial class AsterRestClientSpotV3Api : RestApiClient<AsterEnvironment, AsterV3AuthenticationProvider, AsterCredentials>, IAsterRestClientSpotV3Api
    {
        #region fields 
        protected override IRestMessageHandler MessageHandler { get; } = new AsterRestMessageHandler(AsterErrors.SpotErrors);
        protected override ErrorMapping ErrorMapping => AsterErrors.SpotErrors;
        internal AsterRestClient BaseClient { get; set; }

        public new AsterRestOptions ClientOptions => (AsterRestOptions)base.ClientOptions;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IAsterRestClientSpotV3ApiAccount Account { get; }
        /// <inheritdoc />
        public IAsterRestClientSpotV3ApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IAsterRestClientSpotV3ApiTrading Trading { get; }
        /// <inheritdoc />
        public string ExchangeName => "Aster";
        #endregion

        #region constructor/destructor
        internal AsterRestClientSpotV3Api(AsterRestClient baseClient, ILogger logger, HttpClient? httpClient, AsterRestOptions options)
            : base(logger, 
                  httpClient,
                  options.Environment.SpotRestClientAddress, 
                  options,
                  options.SpotOptions)
        {
            BaseClient = baseClient;

            Account = new AsterRestClientSpotV3ApiAccount(this);
            ExchangeData = new AsterRestClientSpotV3ApiExchangeData(logger, this);
            Trading = new AsterRestClientSpotV3ApiTrading(logger, this);

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
            ArraySerialization = ArrayParametersSerialization.MultipleValues;

            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + baseClient.CryptoExchangeLibVersion }
            };
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(AsterExchange._serializerContext);

        /// <inheritdoc />
        protected override AsterV3AuthenticationProvider CreateAuthenticationProvider(AsterCredentials credentials)
            => new AsterV3AuthenticationProvider(credentials);

        internal async Task<WebCallResult> SendAsync(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null)
        {
            var result = await base.SendAsync(BaseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        internal Task<WebCallResult<T>> SendAsync<T>(RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
            => SendToAddressAsync<T>(BaseAddress, definition, parameters, cancellationToken, weight);

        internal async Task<WebCallResult<T>> SendToAddressAsync<T>(string baseAddress, RequestDefinition definition, ParameterCollection? parameters, CancellationToken cancellationToken, int? weight = null) where T : class
        {
            if (definition.Authenticated)
                await AsterUtils.CheckBuilderFeeAsync(BaseClient).ConfigureAwait(false);

            var result = await base.SendAsync<T>(baseAddress, definition, parameters, cancellationToken, null, weight).ConfigureAwait(false);
            if (!result && result.Error!.ErrorType == ErrorType.InvalidTimestamp && (ApiOptions.AutoTimestamp ?? ClientOptions.AutoTimestamp))
            {
                _logger.Log(LogLevel.Debug, "Received Invalid Timestamp error, triggering new time sync");
                TimeOffsetManager.ResetRestUpdateTime(ClientName);
            }
            return result;
        }

        /// <inheritdoc />
        protected override Task<WebCallResult<DateTime>> GetServerTimestampAsync()
            => ExchangeData.GetServerTimeAsync();

        /// <inheritdoc />
        public override string FormatSymbol(string baseAsset, string quoteAsset, TradingMode tradingMode, DateTime? deliverDate = null) 
            => AsterExchange.FormatSymbol(baseAsset, quoteAsset, tradingMode, deliverDate);

        /// <inheritdoc />
        public IAsterRestClientSpotV3ApiShared SharedClient => this;
    }
}
