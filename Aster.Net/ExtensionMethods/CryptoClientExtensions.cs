using CryptoExchange.Net.Interfaces;
using Aster.Net.Clients;
using Aster.Net.Interfaces.Clients;

namespace CryptoExchange.Net.Interfaces
{
    /// <summary>
    /// Extensions for the ICryptoRestClient and ICryptoSocketClient interfaces
    /// </summary>
    public static class CryptoClientExtensions
    {
        /// <summary>
        /// Get the Aster REST Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IAsterRestClient Aster(this ICryptoRestClient baseClient) => baseClient.TryGet<IAsterRestClient>(() => new AsterRestClient());

        /// <summary>
        /// Get the Aster Websocket Api client
        /// </summary>
        /// <param name="baseClient"></param>
        /// <returns></returns>
        public static IAsterSocketClient Aster(this ICryptoSocketClient baseClient) => baseClient.TryGet<IAsterSocketClient>(() => new AsterSocketClient());
    }
}
