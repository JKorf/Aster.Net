using Aster.Net.Clients.FuturesV3Api;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.FuturesApi;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.FuturesApi
{
    internal partial class AsterSocketClientFuturesSharedApi
    {
        #region Subscribe To Futures Order Updates

        async Task<WebSocketResult<UpdateSubscription>> IFuturesOrderSocketClient.SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrder[]>> handler, CancellationToken ct)
            => await SubscribeToFuturesOrderUpdatesAsync(request, x => handler(x.ToType<SharedFuturesOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeFuturesOrderOptions SubscribeFuturesOrderOptions { get; } 
            = new SubscribeFuturesOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToFuturesOrderUpdatesAsync(SubscribeFuturesOrderRequest request, Action<DataEvent<SharedFuturesOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeFuturesOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                onOrderUpdate: update => handler(update.ToType(new[] {
                    new SharedFuturesOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.UpdateData.Symbol), update.Data.UpdateData.Symbol,
                        update.Data.UpdateData.OrderId.ToString(),
                        ParseOrderType(update.Data.UpdateData.Type),
                        update.Data.UpdateData.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        update.Data.UpdateData.Status == Enums.OrderStatus.Canceled ? SharedOrderStatus.Canceled : (update.Data.UpdateData.Status == Enums.OrderStatus.New || update.Data.UpdateData.Status == Enums.OrderStatus.PartiallyFilled) ? SharedOrderStatus.Open : SharedOrderStatus.Filled,
                        update.Data.UpdateData.UpdateTime)
                    {
                        ClientOrderId = update.Data.UpdateData.ClientOrderId,
                        OrderPrice = update.Data.UpdateData.Price == 0 ? null : update.Data.UpdateData.Price,
                        OrderQuantity = new SharedOrderQuantity(update.Data.UpdateData.Quantity, contractQuantity: update.Data.UpdateData.Quantity),
                        QuantityFilled = new SharedOrderQuantity(update.Data.UpdateData.AccumulatedQuantityOfFilledTrades, contractQuantity : update.Data.UpdateData.AccumulatedQuantityOfFilledTrades),
                        UpdateTime = update.Data.UpdateData.UpdateTime,
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = update.Data.UpdateData.Fee,
                        FeeAsset = update.Data.UpdateData.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        AveragePrice = update.Data.UpdateData.AveragePrice == 0 ? null : update.Data.UpdateData.AveragePrice,
                        PositionSide = update.Data.UpdateData.PositionSide == Enums.PositionSide.Long ? SharedPositionSide.Long : update.Data.UpdateData.PositionSide == Enums.PositionSide.Short ? SharedPositionSide.Short : null,
                        ReduceOnly = update.Data.UpdateData.IsReduce,
                        TimeInForce = update.Data.UpdateData.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.UpdateData.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        TriggerPrice = update.Data.UpdateData.StopPrice == 0 ? null : update.Data.UpdateData.StopPrice,
                        IsTriggerOrder = update.Data.UpdateData.StopPrice > 0,
                        IsCloseOrder = update.Data.UpdateData.IsClosePositionOrder,
                        LastTrade = update.Data.UpdateData.QuantityOfLastFilledTrade == 0 ? null : 
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.UpdateData.Symbol), 
                                update.Data.UpdateData.Symbol,
                                update.Data.UpdateData.OrderId.ToString(),
                                update.Data.UpdateData.TradeId.ToString(),
                                update.Data.UpdateData.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                new SharedOrderQuantity(update.Data.UpdateData.QuantityOfLastFilledTrade), 
                                update.Data.UpdateData.PriceLastFilledTrade,
                                update.Data.UpdateData.UpdateTime)
                            {
                                Role = update.Data.UpdateData.BuyerIsMaker ? SharedRole.Maker : SharedRole.Taker,
                                ClientOrderId = update.Data.UpdateData.ClientOrderId
                            }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market || type == OrderType.StopMarket || type == OrderType.TakeProfitMarket || type == OrderType.TrailingStopMarket)
                return SharedOrderType.Market;

            if (type == OrderType.Limit || type == OrderType.Stop || type == OrderType.TakeProfit)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
    }
}
