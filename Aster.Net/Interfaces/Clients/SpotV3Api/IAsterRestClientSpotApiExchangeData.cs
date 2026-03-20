using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.SpotV3Api
{
    /// <summary>
    /// Aster Spot exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IAsterRestClientSpotV3ApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#get-server-time" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get exchange information
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#trading-specification-information" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get order book snapshot
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#depth-informationn" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results. 5, 10, 20, 50, 100, 500 or 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get recent trades for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#recent-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results. Max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#query-historical-trades-market_data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/historicalTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results. Max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null, CancellationToken ct = default);

        /// <summary>
        /// Get aggregated trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#recent-trades-aggregated" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/aggTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return results after this</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results. Max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAggregateTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#k-line-data" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Max number of results. Max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#24h-price-change" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#24h-price-change" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterSpotTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price info for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#latest-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] Symbol name, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price info for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#latest-price" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice[]>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#current-best-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/spot-v3/market-data/#current-best-order" /><br />
        /// Endpoint:<br />
        /// GET /api/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default);
    }
}
