using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.FuturesV3Api
{
    /// <summary>
    /// Aster Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IAsterRestClientFuturesV3ApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#check-server-time" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/time
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol and asset information
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#exchange-information" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/exchangeInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a snapshot of the current order book
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#order-book" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/depth
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows in the orderbook. 5, 10, 20, 50, 100, 500 or 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of the most recent trades
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#recent-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/trades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#old-trades-lookup-market_data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/historicalTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1000</param>
        /// <param name="fromId">["<c>fromId</c>"] Return from this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get aggregated trade history. Trades are aggregated if they're executed on the same time at the same price.
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#compressedaggregate-trades-list" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/aggTrades
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">["<c>fromId</c>"] Return from this id</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAggregateTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#klinecandlestick-data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/klines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index kline data
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#index-price-klinecandlestick-data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/indexPriceKlines
        /// </para>
        /// </summary>
        /// <param name="index">["<c>pair</c>"] The index</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetIndexPriceKlinesAsync(string index, KlineInterval interval, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price kline data
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#mark-price-klinecandlestick-data" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/markPriceKlines
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">["<c>interval</c>"] Kline interval</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null,
            DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price for a symbol
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#mark-price" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/premiumIndex
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get mark price for all symbols
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#mark-price" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/premiumIndex
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMarkPrice[]>> GetMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#get-funding-rate-history" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/fundingRate
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">["<c>startTime</c>"] Filter by start time</param>
        /// <param name="endTime">["<c>endTime</c>"] Filter by end time</param>
        /// <param name="limit">["<c>limit</c>"] Number of rows to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterFundingRateHistory[]>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate configuration
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#get-funding-rate-config" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/fundingInfo
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterFundingInfo[]>> GetFundingInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#24hr-ticker-price-change-statistics" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/ticker/24hr
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get last price
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get last prices
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#symbol-price-ticker" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/ticker/price
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice[]>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="symbol">["<c>symbol</c>"] The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para>
        /// Docs:<br />
        /// <a href="https://asterdex.github.io/aster-api-website/futures-v3/market-data/#symbol-order-book-ticker" /><br />
        /// Endpoint:<br />
        /// GET /fapi/v3/ticker/bookTicker
        /// </para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default);
    }
}
