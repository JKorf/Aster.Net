using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Interfaces.Clients.SpotApi
{
    /// <summary>
    /// Shared interface for Spot socket API usage
    /// </summary>
    public interface IAsterSocketClientSpotApiShared :
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
