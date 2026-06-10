using Aster.Net.Clients.MessageHandlers;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
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

namespace Aster.Net.Clients.FuturesV3Api
{
    /// <inheritdoc cref="IAsterRestClientFuturesV3Api" />
    internal partial class AsterRestClientFuturesV3Api : RestApiClient<AsterEnvironment, AsterV3AuthenticationProvider, AsterCredentials>, IAsterRestClientFuturesV3Api
    {
        #region fields 
        protected override IRestMessageHandler MessageHandler { get; } = new AsterRestMessageHandler(AsterErrors.FuturesErrors);
        protected override ErrorMapping ErrorMapping => AsterErrors.FuturesErrors;

        internal AsterRestClient BaseClient { get; set; }

        public new AsterRestOptions ClientOptions => (AsterRestOptions)base.ClientOptions;
        #endregion

        #region Api clients
        /// <inheritdoc />
        public IAsterRestClientFuturesV3ApiAccount Account { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesV3ApiExchangeData ExchangeData { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesV3ApiTrading Trading { get; }
        #endregion

        #region constructor/destructor
        internal AsterRestClientFuturesV3Api(AsterRestClient baseClient, ILogger logger, HttpClient? httpClient, AsterRestOptions options)
            : base(logger,
                  AsterExchange.Metadata.Id,
                  httpClient,
                  options.Environment.FuturesRestClientAddress,
                  options,
                  options.FuturesOptions)
        {
            BaseClient = baseClient;

            Account = new AsterRestClientFuturesV3ApiAccount(this);
            ExchangeData = new AsterRestClientFuturesV3ApiExchangeData(logger, this);
            Trading = new AsterRestClientFuturesV3ApiTrading(logger, this);

            StandardRequestHeaders = new Dictionary<string, string>
            {
                { "User-Agent", "CryptoExchange.Net/" + baseClient.CryptoExchangeLibVersion }
            };

            RequestBodyEmptyContent = "";
            RequestBodyFormat = RequestBodyFormat.FormData;
        }
        #endregion

        /// <inheritdoc />
        protected override IMessageSerializer CreateSerializer() => new SystemTextJsonMessageSerializer(AsterExchange._serializerContext);

        /// <inheritdoc />
        protected override AsterV3AuthenticationProvider CreateAuthenticationProvider(AsterCredentials credentials)
            => new AsterV3AuthenticationProvider(credentials);

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

        internal async Task<HttpResult<T>> SendAsync<T>(RequestDefinition definition, Parameters? parameters, CancellationToken cancellationToken, int? weight = null, bool checkBuilderFee = true) where T : class
        {
            if (checkBuilderFee && definition.Authenticated)
                await AsterUtils.CheckBuilderFeeAsync(BaseClient).ConfigureAwait(false);

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
        public IAsterRestClientFuturesV3ApiShared SharedClient => this;

    }
}
