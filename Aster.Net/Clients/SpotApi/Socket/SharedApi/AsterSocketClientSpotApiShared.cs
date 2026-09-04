using Aster.Net.Clients.FuturesV3Api;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotApi
{
    internal partial class AsterSocketClientSpotSharedApi :
        SharedApiBase,
        IAsterSocketClientSpotApiShared,
        IAsterSocketClientSpotSharedApi
    {
        private readonly AsterSocketClientSpotApi _api;

        private const string _topicId = "AsterSpot";
        private const string _exchangeName = "Aster";

        public override SharedClientInfo Discover() => SharedUtils.GetClientInfo(AsterExchange.Metadata, this);

        public AsterSocketClientSpotSharedApi(AsterSocketClientSpotApi api)
            : base(
                  SharedTransport.Socket,
                  api.Exchange,
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
    }
}
