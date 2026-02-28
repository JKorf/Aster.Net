using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Aster Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IAsterRestClientSpotApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#get-server-time" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange information
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#trading-specification-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get order book snapshot
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#depth-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results. 5, 10, 20, 50, 100, 500 or 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#recent-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="limit">Max number of results. Max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#query-historical-trades-market_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/historicalTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="fromId">Return results after this</param>
        /// <param name="limit">Max number of results. Max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get aggregated trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#query-historical-trades-market_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/aggTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="fromId">Return results after this</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results. Max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAggregateTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#k-line-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Max number of results. Max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#24h-price-change" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#24h-price-change" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price info for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#latest-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">Symbol name, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price info for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#latest-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker/price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice[]>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#current-best-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-spot-api.md#current-best-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v1/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default);
    }
}
