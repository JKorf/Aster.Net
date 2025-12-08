using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Objects;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesApi
{
    /// <inheritdoc />
    internal class AsterRestClientFuturesApiExchangeData : IAsterRestClientFuturesApiExchangeData
    {
        private readonly AsterRestClientFuturesApi _baseClient;
        private static readonly RequestDefinitionCache _definitions = new RequestDefinitionCache();

        internal AsterRestClientFuturesApiExchangeData(ILogger logger, AsterRestClientFuturesApi baseClient)
        {
            _baseClient = baseClient;
        }

        #region Get Server Time

        /// <inheritdoc />
        public async Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/time", AsterExchange.RateLimiter.RestIp, 1, false);
            var result = await _baseClient.SendAsync<AsterServerTime>(request, null, ct).ConfigureAwait(false);
            return result.As(result.Data?.ServerTime ?? default);
        }

        #endregion

        #region Exchange Information

        /// <inheritdoc />
        public async Task<WebCallResult<AsterExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/exchangeInfo", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync<AsterExchangeInfo>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Order Book

        /// <inheritdoc />
        public async Task<WebCallResult<AsterOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntValues(nameof(limit), 5, 10, 20, 50, 100, 500, 1000);
            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 10 : limit <= 50 ? 2 : limit == 100 ? 5 : limit == 500 ? 10 : 20;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/depth", AsterExchange.RateLimiter.RestIp, requestWeight);
            var result = await _baseClient.SendAsync<AsterOrderBook>(request, parameters, ct, requestWeight).ConfigureAwait(false);
            if (result && string.IsNullOrEmpty(result.Data.Symbol))
                result.Data.Symbol = symbol;
            return result.As(result.Data);
        }

        #endregion

        #region Get Recent Trades
        /// <inheritdoc />
        public async Task<WebCallResult<AsterRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/trades", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync<AsterRecentTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Trade History
        /// <inheritdoc />
        public async Task<WebCallResult<AsterRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/historicalTrades", AsterExchange.RateLimiter.RestIp, 20, true);
            return await _baseClient.SendAsync<AsterRecentTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }
        #endregion

        #region Aggregate Trades List

        /// <inheritdoc />
        public async Task<WebCallResult<AsterAggregateTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);

            var parameters = new ParameterCollection { { "symbol", symbol } };
            parameters.AddOptionalParameter("fromId", fromId?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/aggTrades", AsterExchange.RateLimiter.RestIp, 20);
            return await _baseClient.SendAsync<AsterAggregateTrade[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Klines

        /// <inheritdoc />
        public async Task<WebCallResult<AsterKline[]>> GetKlinesAsync(string symbol, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new ParameterCollection {
                { "symbol", symbol },
            };
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/klines", AsterExchange.RateLimiter.RestIp, requestWeight);
            return await _baseClient.SendAsync<AsterKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Get Index Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<AsterKline[]>> GetIndexPriceKlinesAsync(string index, KlineInterval interval, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);
            var parameters = new ParameterCollection {
                { "pair", index },
            };
            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/indexPriceKlines", AsterExchange.RateLimiter.RestIp, requestWeight);
            return await _baseClient.SendAsync<AsterKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Get Mark Price Klines

        /// <inheritdoc />
        public async Task<WebCallResult<AsterKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null, DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1500);

            var parameters = new ParameterCollection {
                { "symbol", symbol },
            };

            parameters.AddEnum("interval", interval);
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));

            var requestWeight = limit == null ? 5 : limit <= 100 ? 1 : limit <= 500 ? 2 : limit <= 1000 ? 5 : 10;
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/markPriceKlines", AsterExchange.RateLimiter.RestIp, requestWeight);
            return await _baseClient.SendAsync<AsterKline[]>(request, parameters, ct, requestWeight).ConfigureAwait(false);
        }

        #endregion

        #region Get Mark Price

        /// <inheritdoc />
        public async Task<WebCallResult<AsterMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/premiumIndex", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync<AsterMarkPrice>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<AsterMarkPrice[]>> GetMarkPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/premiumIndex", AsterExchange.RateLimiter.RestIp, 10);
            return await _baseClient.SendAsync<AsterMarkPrice[]>(request, null, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get Funding Rate History

        /// <inheritdoc />
        public async Task<WebCallResult<AsterFundingRateHistory[]>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default)
        {
            limit?.ValidateIntBetween(nameof(limit), 1, 1000);
            var parameters = new ParameterCollection {
                { "symbol", symbol }
            };
            parameters.AddOptionalParameter("startTime", DateTimeConverter.ConvertToMilliseconds(startTime));
            parameters.AddOptionalParameter("endTime", DateTimeConverter.ConvertToMilliseconds(endTime));
            parameters.AddOptionalParameter("limit", limit?.ToString(CultureInfo.InvariantCulture));

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/fundingRate", AsterExchange.RateLimiter.RestIp, 1, false);
            return await _baseClient.SendAsync<AsterFundingRateHistory[]>(request, parameters, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Funding Info

        /// <inheritdoc />
        public async Task<WebCallResult<AsterFundingInfo[]>> GetFundingInfoAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/fundingInfo", AsterExchange.RateLimiter.RestIp, 0);
            return await _baseClient.SendAsync<AsterFundingInfo[]>(request, null, ct).ConfigureAwait(false);
        }

        #endregion

        #region Get Ticker
        /// <inheritdoc />
        public async Task<WebCallResult<AsterTicker>> GetTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection();
            parameters.AddOptionalParameter("symbol", symbol);

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/24hr", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync<AsterTicker>(request, parameters, ct).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<AsterTicker[]>> GetTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/24hr", AsterExchange.RateLimiter.RestIp, 40);
            return await _baseClient.SendAsync<AsterTicker[]>(request, null, ct).ConfigureAwait(false);
        }
        #endregion

        #region Get price

        /// <inheritdoc />
        public async Task<WebCallResult<AsterPrice>> GetPriceAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/price", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync<AsterPrice>(request, parameters, ct, 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<AsterPrice[]>> GetPricesAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/price", AsterExchange.RateLimiter.RestIp, 2);
            return await _baseClient.SendAsync<AsterPrice[]>(request, null, ct, 2).ConfigureAwait(false);
        }
        #endregion

        #region Get Book Ticker

        /// <inheritdoc />
        public async Task<WebCallResult<AsterBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default)
        {
            var parameters = new ParameterCollection
            {
                { "symbol", symbol }
            };

            var request = _definitions.GetOrCreate(HttpMethod.Get, "/fapi/v1/ticker/bookTicker", AsterExchange.RateLimiter.RestIp, 1);
            return await _baseClient.SendAsync<AsterBookTicker>(request, parameters, ct, 1).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task<WebCallResult<AsterBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default)
        {
            var request = _definitions.GetOrCreate(HttpMethod.Get, "fapi/v1/ticker/bookTicker", AsterExchange.RateLimiter.RestIp, 2);
            return await _baseClient.SendAsync<AsterBookTicker[]>(request, null, ct, 2).ConfigureAwait(false);
        }

        #endregion
    }
}
