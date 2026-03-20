using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Interfaces.Clients.FuturesV3Api
{
    /// <summary>
    /// Shared interface for Futures rest API usage
    /// </summary>
    public interface IAsterRestClientFuturesV3ApiShared :
        IBalanceRestClient,
        IFuturesTickerRestClient,
        IFuturesSymbolRestClient,
        IFuturesOrderRestClient,
        IKlineRestClient,
        IRecentTradeRestClient,
        ITradeHistoryRestClient,
        ILeverageRestClient,
        IMarkPriceKlineRestClient,
        IIndexPriceKlineRestClient,
        IOrderBookRestClient,
        IFundingRateRestClient,
        IPositionModeRestClient,
        IListenKeyRestClient,
        IFeeRestClient,
        IFuturesOrderClientIdRestClient,
        IFuturesTriggerOrderRestClient,
        IFuturesTpSlRestClient,
        IBookTickerRestClient
    {
    }
}
