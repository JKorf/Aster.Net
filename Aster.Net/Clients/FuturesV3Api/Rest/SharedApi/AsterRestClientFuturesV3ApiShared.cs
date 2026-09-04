using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Objects.Models;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Errors;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesV3Api
{
    internal partial class AsterRestClientFuturesV3SharedApi :
        SharedApiBase,
        IAsterRestClientFuturesV3ApiShared,
        IAsterRestClientFuturesV3SharedApi
    {
        private readonly AsterRestClientFuturesV3Api _api;

        private const string _topicId = "AsterFutures";
        private const string _exchangeName = "Aster";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(AsterExchange.Metadata, this);

        public AsterRestClientFuturesV3SharedApi(AsterRestClientFuturesV3Api api)
            : base(
                  SharedTransport.Rest,
                  api.Exchange,
                  [TradingMode.PerpetualLinear],
                  () => api.Authenticated,
                  api.FormatSymbol)
        {
            _api = api;

            SetCapabilities(
                GetKlinesOptions,
                GetMarkPriceKlinesOptions,
                GetFuturesSymbolsOptions,
                GetFuturesTickerOptions,
                GetAllFuturesTickersOptions,
                GetBookTickerOptions,
                GetRecentTradesOptions,
                PlaceFuturesOrderOptions,
                GetFuturesOrderOptions,
                GetOpenFuturesOrdersOptions,
                GetClosedFuturesOrdersOptions,
                CancelFuturesOrderOptions,
                GetFuturesOrderTradesOptions,
                GetFuturesUserTradeHistoryOptions,
                GetPositionsOptions,
                ClosePositionOptions,
                GetFuturesOrderByClientOrderIdOptions,
                CancelFuturesOrderByClientOrderIdOptions,
                GetLeverageOptions,
                SetLeverageOptions,
                GetOrderBookOptions,
                GetTradeHistoryOptions,
                GetIndexPriceKlinesOptions,
                GetFundingRateHistoryOptions,
                GetBalancesOptions,
                GetPositionModeOptions,
                SetPositionModeOptions,
                GetFeeOptions,
                PlaceFuturesTriggerOrderOptions,
                GetFuturesTriggerOrderOptions,
                CancelFuturesTriggerOrderOptions,
                SetFuturesTpSlOptions,
                CancelFuturesTpSlOptions
                );
        }
    }
}
