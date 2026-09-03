using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Interfaces.Clients.FuturesApi
{
    /// <summary>
    /// Shared interface for Futures socket API usage
    /// </summary>
    public interface IAsterSocketClientFuturesApiShared :
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

    /// <summary>
    /// Shared API interface. Shared APIs provide a common,
    /// exchange-independent contract for accessing functionality across different
    /// exchange client libraries.
    /// </summary>
    public interface IAsterSocketClientFuturesSharedApi :
        ISubscribeAllTickersSocket,
        ISubscribeTickerSocket,
        ISubscribeTradesSocket,
        ISubscribeBookTickerSocket,
        ISubscribeOrderBookSocket,
        ISubscribeKlinesSocket,
        ISubscribeBalancesSocket,
        ISubscribeFuturesOrdersSocket,
        ISubscribePositionsSocket
    { }
}
