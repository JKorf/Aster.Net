using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Clients.SpotV3Api
{
    internal partial class AsterRestClientSpotV3SharedApi :
        SharedApiBase,
        IAsterRestClientSpotV3ApiShared,
        IAsterRestClientSpotV3SharedApi
    {
        private readonly AsterRestClientSpotV3Api _api;

        private const string _topicId = "AsterSpot";
        private const string _exchangeName = "Aster";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(AsterExchange.Metadata, this);


        public AsterRestClientSpotV3SharedApi(AsterRestClientSpotV3Api api)
            : base(
                  SharedTransport.Rest,
                  api,
                  [TradingMode.Spot],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetSpotSymbolsOptions,
                GetTickerOptions,
                GetAllTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                GetTradeHistoryOptions,
                GetOrderBookOptions,
                GetBalancesOptions,
                PlaceSpotOrderOptions,
                GetSpotOrderOptions,
                GetOpenSpotOrdersOptions,
                GetClosedSpotOrdersOptions,
                GetSpotOrderTradesOptions,
                CancelSpotOrderOptions,
                GetSpotUserTradeHistoryOptions,
                GetSpotOrderByClientOrderIdOptions,
                CancelSpotOrderByClientOrderIdOptions,
                GetAssetOptions,
                GetAllAssetsOptions,
                GetFeeOptions,
                PlaceSpotTriggerOrderOptions,
                GetSpotTriggerOrderOptions,
                CancelSpotTriggerOrderOptions,
                TransferOptions
                );
        }
    }
}
