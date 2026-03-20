using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Interfaces.Clients.SpotV3Api
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IAsterSocketClientSpotV3ApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ISpotOrderSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IBalanceSocketClient,
        IKlineSocketClient,
        IOrderBookSocketClient
    {
    }
}
