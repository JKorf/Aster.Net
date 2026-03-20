using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Interfaces.Clients.SpotV3Api
{
    /// <summary>
    /// Shared interface for Spot rest API usage
    /// </summary>
    public interface IAsterRestClientSpotV3ApiShared :
        IAssetsRestClient,
        IBalanceRestClient,
        IKlineRestClient,
        IOrderBookRestClient,
        IRecentTradeRestClient,
        ISpotOrderRestClient,
        ISpotSymbolRestClient,
        ISpotTickerRestClient,
        ITradeHistoryRestClient,
        IListenKeyRestClient,
        IFeeRestClient,
        ISpotOrderClientIdRestClient,
        ISpotTriggerOrderRestClient,
        IBookTickerRestClient,
        ITransferRestClient
    {
    }
}
