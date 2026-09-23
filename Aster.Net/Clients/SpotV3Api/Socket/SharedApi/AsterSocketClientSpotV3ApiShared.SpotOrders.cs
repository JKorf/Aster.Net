using Aster.Net.Clients.SpotApi;
using Aster.Net.Enums;
using Aster.Net.Interfaces.Clients.SpotApi;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Aster.Net.Clients.SpotV3Api
{
    internal partial class AsterSocketClientSpotV3SharedApi
    {
        #region Subscribe To Spot Order Updates

        async Task<WebSocketResult<UpdateSubscription>> ISpotOrderSocketClient.SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrder[]>> handler, CancellationToken ct)
            => await SubscribeToSpotOrderUpdatesAsync(request, x => handler(x.ToType<SharedSpotOrder[]>(x.Data)), ct).ConfigureAwait(false);

        public SubscribeSpotOrderOptions SubscribeSpotOrderOptions { get; } 
            = new SubscribeSpotOrderOptions(_exchangeName, true);
        public async Task<WebSocketResult<UpdateSubscription>> SubscribeToSpotOrderUpdatesAsync(SubscribeSpotOrderRequest request, Action<DataEvent<SharedSpotOrderUpdate[]>> handler, CancellationToken ct)
        {
            var validationError = SubscribeSpotOrderOptions.ValidateRequest(request, this);
            if (validationError != null)
                return WebSocketResult.Fail<UpdateSubscription>(_exchangeName, validationError);

            var result = await _api.SubscribeToUserDataUpdatesAsync(
                onOrderUpdate: update => handler(update.ToType(new[] {
                    new SharedSpotOrderUpdate(
                        ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                        update.Data.Symbol,
                        update.Data.Id.ToString(),
                        ParseOrderType(update.Data.Type),
                        update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                        ParseOrderStatus(update.Data.Status),
                        update.Data.CreateTime)
                    {
                        ClientOrderId = update.Data.ClientOrderId,
                        OrderPrice = update.Data.Price,
                        OrderQuantity = new SharedOrderQuantity(update.Data.Quantity, update.Data.QuoteQuantity == 0 ? null : update.Data.QuoteQuantity),
                        QuantityFilled = new SharedOrderQuantity(update.Data.QuantityFilled, update.Data.QuoteQuantityFilled),
                        UpdateTime = update.Data.UpdateTime,
#pragma warning disable CS0618 // Type or member is obsolete
                        Fee = update.Data.Fee,
                        FeeAsset = update.Data.FeeAsset,
#pragma warning restore CS0618 // Type or member is obsolete
                        TimeInForce = update.Data.TimeInForce == Enums.TimeInForce.ImmediateOrCancel ? SharedTimeInForce.ImmediateOrCancel : update.Data.TimeInForce == Enums.TimeInForce.FillOrKill ? SharedTimeInForce.FillOrKill : SharedTimeInForce.GoodTillCanceled,
                        TriggerPrice = update.Data.StopPrice == 0 ? null : update.Data.StopPrice,
                        IsTriggerOrder = update.Data.StopPrice > 0,
                        LastTrade = update.Data.LastQuantityFilled == 0 ? null :
                            new SharedUserTrade(
                                ExchangeSymbolCache.ParseSymbol(_topicId, _api.EnvironmentName, null, update.Data.Symbol),
                                update.Data.Symbol, 
                                update.Data.Id.ToString(), 
                                update.Data.TradeId.ToString(), 
                                update.Data.Side == Enums.OrderSide.Buy ? SharedOrderSide.Buy : SharedOrderSide.Sell,
                                new SharedOrderQuantity(update.Data.LastQuantityFilled),
                                update.Data.LastPriceFilled, 
                                update.Data.UpdateTime)
                            {
                                ClientOrderId = update.Data.ClientOrderId,
                                Role = update.Data.BuyerIsMaker ? SharedRole.Maker : SharedRole.Taker
                            }
                    }
                })),
                ct: ct).ConfigureAwait(false);

            return result;
        }

        #endregion

        private SharedOrderStatus ParseOrderStatus(OrderStatus status)
        {
            if (status == OrderStatus.Filled)
                return SharedOrderStatus.Filled;

            if (status == OrderStatus.PartiallyFilled
                || status == OrderStatus.New)
            {
                return SharedOrderStatus.Open;
            }

            if (status == OrderStatus.Canceled
                || status == OrderStatus.Expired
                || status == OrderStatus.Rejected)
            {
                return SharedOrderStatus.Canceled;
            }

            return SharedOrderStatus.Unknown;
        }

        private SharedOrderType ParseOrderType(OrderType type)
        {
            if (type == OrderType.Market || type == OrderType.TakeProfitMarket || type == OrderType.StopMarket)
                return SharedOrderType.Market;

            if (type == OrderType.Limit || type == OrderType.TakeProfit|| type == OrderType.Stop)
                return SharedOrderType.Limit;

            return SharedOrderType.Other;
        }
    }
}
