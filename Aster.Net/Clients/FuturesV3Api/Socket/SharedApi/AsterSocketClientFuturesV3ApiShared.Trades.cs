using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesV3Api
{
    internal partial class AsterSocketClientFuturesV3SharedApi
    {
        #region Subscribe To Trade Updates

        public SubscribeTradeOptions SubscribeTradeOptions { get; } 
            = new SubscribeTradeOptions(_exchangeName, false)
        {
            SupportsMultipleSymbols = true,
            MaxSymbolCount = 200
        };
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(SubscribeTradeRequest request, Action<DataEvent<SharedTrade[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeTradeOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var symbols = request.Symbols?.Length > 0 ? request.Symbols.Select(x => x.GetSymbol(FormatSymbol)).ToArray() : [request.Symbol!.GetSymbol(FormatSymbol)];
            var result = await _api.SubscribeToAggregatedTradeUpdatesAsync(symbols, update => handler(update.ToType(new[] { 
                new SharedTrade(
                    ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol), 
                    update.Data.Symbol,
                    new SharedOrderQuantity(update.Data.Quantity),
                    update.Data.Price, 
                    update.Data.TradeTime)
            {
                Side = update.Data.BuyerIsMaker ? SharedOrderSide.Sell : SharedOrderSide.Buy,
            } })), ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

    }
}
