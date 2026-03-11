using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IAsterSocketClientFuturesV3ApiShared :
        ITickerSocketClient,
        ITickersSocketClient,
        ITradeSocketClient,
        IBookTickerSocketClient,
        IOrderBookSocketClient,
        IKlineSocketClient,
        IBalanceSocketClient,
        IPositionSocketClient,
        IFuturesOrderSocketClient
    {
    }
}
