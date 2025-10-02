using Aster.Net.Enums;
using Aster.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Aster Futures exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.
    /// </summary>
    public interface IAsterRestClientFuturesApiExchangeData
    {
        /// <summary>
        /// Get the current server time
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#check-server-time" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);

        /// <summary>
        /// Get symbol and asset information
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#exchange-information" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterExchangeInfo>> GetExchangeInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get a snapshot of the current order book
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#order-book" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">Number of rows in the orderbook. 5, 10, 20, 50, 100, 500 or 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterOrderBook>> GetOrderBookAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get list of the most recent trades
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#recent-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">Number of rows to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterRecentTrade[]>> GetRecentTradesAsync(string symbol, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get trade history
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#old-trades-lookup-market_data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="limit">Number of rows to return, max 1000</param>
        /// <param name="fromId">Return from this id</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterRecentTrade[]>> GetTradeHistoryAsync(string symbol, int? limit = null, long? fromId = null,
            CancellationToken ct = default);

        /// <summary>
        /// Get aggregated trade history. Trades are aggregated if they're executed on the same time at the same price.
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#compressedaggregate-trades-list" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="fromId">Return from this id</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterAggregateTrade[]>> GetAggregatedTradeHistoryAsync(string symbol, long? fromId = null, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get kline/candlestick data
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows to return, max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetKlinesAsync(string symbol, KlineInterval interval,
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get index kline data
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-data" /></para>
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows to return, max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetIndexPriceKlinesAsync(string index, KlineInterval interval, 
            DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price kline data
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#klinecandlestick-data" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="interval">Kline interval</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows to return, max 1500</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterKline[]>> GetMarkPriceKlinesAsync(string symbol, KlineInterval interval, int? limit = null,
            DateTime? startTime = null, DateTime? endTime = null, CancellationToken ct = default);

        /// <summary>
        /// Get mark price for a symbol
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#mark-price" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMarkPrice>> GetMarkPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get mark price for all symbols
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#mark-price" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterMarkPrice[]>> GetMarkPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get funding rate history
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#get-funding-rate-history" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="startTime">Filter by start time</param>
        /// <param name="endTime">Filter by end time</param>
        /// <param name="limit">Number of rows to return, max 1000</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterFundingRateHistory[]>> GetFundingRatesAsync(string symbol, DateTime? startTime = null, DateTime? endTime = null, int? limit = null, CancellationToken ct = default);

        /// <summary>
        /// Get funding rate configuration
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#get-funding-rate-history" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterFundingInfo[]>> GetFundingInfoAsync(CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTicker>> GetTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get price ticker info
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#24hr-ticker-price-change-statistics" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterTicker[]>> GetTickersAsync(CancellationToken ct = default);

        /// <summary>
        /// Get last price
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice>> GetPriceAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get last prices
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#symbol-price-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterPrice[]>> GetPricesAsync(CancellationToken ct = default);

        /// <summary>
        /// Get book ticker
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="symbol">The symbol, for example `ETHUSDT`</param>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker>> GetBookTickerAsync(string symbol, CancellationToken ct = default);

        /// <summary>
        /// Get book tickers
        /// <para><a href="https://github.com/asterdex/api-docs/blob/master/aster-finance-futures-api.md#symbol-order-book-ticker" /></para>
        /// </summary>
        /// <param name="ct">Cancellation token</param>
        Task<WebCallResult<AsterBookTicker[]>> GetBookTickersAsync(CancellationToken ct = default);
    }
}
