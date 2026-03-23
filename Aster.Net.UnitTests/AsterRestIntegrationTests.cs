using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Aster.Net.Clients;
using Aster.Net.Objects.Options;
using Aster.Net.Objects;

namespace Aster.Net.UnitTests
{
    [NonParallelizable]
    public class AsterRestIntegrationTests : RestIntegrationTest<AsterRestClient>
    {
        public override bool Run { get; set; } = false;

        public override AsterRestClient GetClient(ILoggerFactory loggerFactory)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new AsterRestClient(null, loggerFactory, Options.Create(new AsterRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                ApiCredentials = Authenticated ? new AsterCredentials(new AsterV3Credential(key, sec)) : null
            }));
        }

        [Test]
        public async Task TestErrorResponseParsing()
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient().SpotApi.ExchangeData.GetTickerAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(-1121));
        }

        [Test]
        public async Task TestSpotAccount()
        {
            await RunAndCheckResult(client => client.SpotV3Api.Account.GetUserCommissionRateAsync("ETHUSDT", default, default), true, true);
            await RunAndCheckResult(client => client.SpotV3Api.Account.GetAccountInfoAsync(default, default), true, true);
        }

        [Test]
        public async Task TestSpotExchangeData()
        {
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetExchangeInfoAsync(default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetOrderBookAsync("ETHUSDT", default, default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false, true, ignoreProperties: ["baseQty"]);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetTradeHistoryAsync("ETHUSDT", default, default, default), true, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", default, default, default, default, default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetTickerAsync("ETHUSDT", default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetTickersAsync(default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetPriceAsync("ETHUSDT", default), false, true);
            await RunAndCheckResult(client => client.SpotV3Api.ExchangeData.GetBookTickerAsync("ETHUSDT", default), false, true);
        }

        [Test]
        public async Task TestSpotTrading()
        {
            await RunAndCheckResult(client => client.SpotV3Api.Trading.GetOpenOrdersAsync("ETHUSDT", default, default), true, true);
            await RunAndCheckResult(client => client.SpotV3Api.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default, default, default), true, true);
            await RunAndCheckResult(client => client.SpotV3Api.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default), true, true, ignoreProperties: ["createUpdateId"]);
        }

        [Test]
        public async Task TestFuturesAccount()
        {
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetPositionModeAsync(default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetMultiAssetModeAsync(default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetBalancesAsync(default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetPositionMarginChangeHistoryAsync("ETHUSDT", default, default, default, default, default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetIncomeHistoryAsync(default, default, default, default, default, default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetLeverageBracketsAsync("ETHUSDT", default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetPositionAdlQuantileEstimationAsync(default, default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetUserCommissionRateAsync("ETHUSDT", default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Account.GetAccountInfoAsync(default, default), true, true);
        }

        [Test]
        public async Task TestFuturesExchangeData()
        {
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetExchangeInfoAsync(default), false, true, ignoreProperties: ["futuresType", "symbolType", "tradingMode", "name", "channel", "imn", "sequenceNo", "tags"]);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetOrderBookAsync("ETHUSDT", default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetTradeHistoryAsync("ETHUSDT", default, default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", default, default, default, default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetMarkPriceAsync("ETHUSDT", default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetFundingRatesAsync("ETHUSDT", default, default, default, default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetFundingInfoAsync(default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetTickerAsync("ETHUSDT", default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetTickersAsync(default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetPriceAsync("ETHUSDT", default), false, true);
            await RunAndCheckResult(client => client.FuturesV3Api.ExchangeData.GetBookTickerAsync("ETHUSDT", default), false, true);
        }

        [Test]
        public async Task TestFuturesTrading()
        {
            await RunAndCheckResult(client => client.FuturesV3Api.Trading.GetPositionsAsync("ETHUSDT", default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Trading.GetForcedOrdersAsync("ETHUSDT", default, default, default, default, default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Trading.GetOpenOrdersAsync("ETHUSDT", default, default), true, true);
            await RunAndCheckResult(client => client.FuturesV3Api.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default, default, default), true, true, ignoreProperties: ["newChainData"]);
            await RunAndCheckResult(client => client.FuturesV3Api.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default), true, true);
        }
    }
}
