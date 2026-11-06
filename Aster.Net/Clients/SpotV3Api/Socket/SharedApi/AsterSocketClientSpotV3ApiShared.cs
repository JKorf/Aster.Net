using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.SharedApis;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotV3Api
{
    internal partial class AsterSocketClientSpotV3SharedApi : 
        SharedApiBase,
        IAsterSocketClientSpotV3ApiShared,
        IAsterSocketClientSpotV3SharedApi
    {
        private readonly AsterSocketClientSpotV3Api _api;

        private const string _topicId = "AsterSpot";
        private const string _exchangeName = "Aster";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(AsterExchange.Metadata, this);

        public AsterSocketClientSpotV3SharedApi(AsterSocketClientSpotV3Api api)
            : base(
                  SharedTransport.Socket,
                  api,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                SubscribeAllTickersOptions,
                SubscribeTickerOptions,
                SubscribeTradeOptions,
                SubscribeBookTickerOptions,
                SubscribeBalanceOptions,
                SubscribeSpotOrderOptions,
                SubscribeKlineOptions,
                SubscribeOrderBookOptions
                );
        }

        /// <inheritdoc />
        public Task UnsubscribeAllAsync() => _api.UnsubscribeAllAsync();
    }
}
