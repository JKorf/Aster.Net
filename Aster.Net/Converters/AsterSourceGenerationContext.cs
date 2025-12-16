using Aster.Net.Enums;
using Aster.Net.Objects.Internal;
using Aster.Net.Objects.Models;
using Aster.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aster.Net.Converters
{
    [JsonSerializable(typeof(SymbolFilterType))]
    [JsonSerializable(typeof(AsterServerTime))]
    [JsonSerializable(typeof(AsterExchangeInfo))]
    [JsonSerializable(typeof(AsterOrderBook))]
    [JsonSerializable(typeof(AsterRecentTrade[]))]
    [JsonSerializable(typeof(AsterAggregateTrade[]))]
    [JsonSerializable(typeof(AsterKline[]))]
    [JsonSerializable(typeof(AsterMarkPrice))]
    [JsonSerializable(typeof(AsterMarkPrice[]))]
    [JsonSerializable(typeof(AsterFundingRateHistory[]))]
    [JsonSerializable(typeof(AsterFundingInfo[]))]
    [JsonSerializable(typeof(AsterTicker))]
    [JsonSerializable(typeof(AsterTicker[]))]
    [JsonSerializable(typeof(AsterPrice))]
    [JsonSerializable(typeof(AsterPrice[]))]
    [JsonSerializable(typeof(AsterBookTicker))]
    [JsonSerializable(typeof(AsterBookTicker[]))]
    [JsonSerializable(typeof(AsterResult))]
    [JsonSerializable(typeof(AsterPositionMode))]
    [JsonSerializable(typeof(AsterOrder))]
    [JsonSerializable(typeof(AsterOrder[]))]
    [JsonSerializable(typeof(AsterOrderResult[]))]
    [JsonSerializable(typeof(AsterMultiAssetMode))]
    [JsonSerializable(typeof(AsterTransferResult))]
    [JsonSerializable(typeof(AsterCountDownResult))]
    [JsonSerializable(typeof(AsterBalance[]))]
    [JsonSerializable(typeof(AsterAccountInfo))]
    [JsonSerializable(typeof(AsterLeverage))]
    [JsonSerializable(typeof(AsterMarginChange[]))]
    [JsonSerializable(typeof(AsterPosition[]))]
    [JsonSerializable(typeof(AsterIncome[]))]
    [JsonSerializable(typeof(AsterSymbolBracket))]
    [JsonSerializable(typeof(AsterSymbolBracket[]))]
    [JsonSerializable(typeof(AsterUserTrade[]))]
    [JsonSerializable(typeof(AsterQuantileEstimation[]))]
    [JsonSerializable(typeof(AsterQuantileEstimation))]
    [JsonSerializable(typeof(AsterUserCommission))]
    [JsonSerializable(typeof(AsterListenKey))]

    [JsonSerializable(typeof(AsterSpotExchangeInfo))]
    [JsonSerializable(typeof(AsterSpotRecentTrade[]))]
    [JsonSerializable(typeof(AsterSpotOrder))]
    [JsonSerializable(typeof(AsterSpotOrder[]))]
    [JsonSerializable(typeof(AsterWithdrawFee))]
    [JsonSerializable(typeof(AsterSpotAccountInfo))]
    [JsonSerializable(typeof(AsterSpotUserTrade[]))]
    [JsonSerializable(typeof(AsterSpotTicker))]
    [JsonSerializable(typeof(AsterSpotTicker[]))]

    [JsonSerializable(typeof(AsterCombinedStream<AsterAggregatedTradeUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterMarkPriceUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterMarkPriceUpdate[]>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterKlineUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterMiniTickUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterMiniTickUpdate[]>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterTickerUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterTickerUpdate[]>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterBookTickerUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterLiquidationUpdateEvent>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterOrderBookUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterMarginUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterConfigUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterOrderUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterAccountUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterSocketEvent>))]

    [JsonSerializable(typeof(AsterCombinedStream<AsterSpotAccountUpdate>))]
    [JsonSerializable(typeof(AsterCombinedStream<AsterSpotOrderUpdate>))]

    [JsonSerializable(typeof(AsterCombinedStream<AsterStreamMinimalTrade>))]

    [JsonSerializable(typeof(AsterSocketRequest))]
    [JsonSerializable(typeof(AsterSocketQueryResponse))]

    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(int?))]
    [JsonSerializable(typeof(int))]
    [JsonSerializable(typeof(long?))]
    [JsonSerializable(typeof(long))]
    [JsonSerializable(typeof(decimal))]
    [JsonSerializable(typeof(decimal?))]
    [JsonSerializable(typeof(DateTime))]
    [JsonSerializable(typeof(DateTime?))]
    [JsonSerializable(typeof(List<Dictionary<string, object>>))]
    [JsonSerializable(typeof(Dictionary<string, object>))]
    internal partial class AsterSourceGenerationContext : JsonSerializerContext
    {
    }
}
