using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using Aster.Net.Clients;
using Aster.Net.Objects.Options;

namespace Aster.Net.UnitTests
{
    [NonParallelizable]
    public class AsterRestIntegrationTests : RestIntegrationTest<AsterRestClient>
    {
        public override bool Run { get; set; } = true;

        public override AsterRestClient GetClient(ILoggerFactory loggerFactory, bool newDeserialization)
        {
            var key = Environment.GetEnvironmentVariable("APIKEY");
            var sec = Environment.GetEnvironmentVariable("APISECRET");

            Authenticated = key != null && sec != null;
            return new AsterRestClient(null, loggerFactory, Options.Create(new AsterRestOptions
            {
                AutoTimestamp = false,
                OutputOriginalData = true,
                UseUpdatedDeserialization = newDeserialization,
                ApiCredentials = Authenticated ? new CryptoExchange.Net.Authentication.ApiCredentials(key, sec) : null
            }));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestErrorResponseParsing(bool newDeserialization)
        {
            if (!ShouldRun())
                return;

            var result = await CreateClient(newDeserialization).SpotApi.ExchangeData.GetTickerAsync("TSTTST", default);

            Assert.That(result.Success, Is.False);
            Assert.That(result.Error.Code, Is.EqualTo(-1121));
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotAccount(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetUserCommissionRateAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetWithdrawFeeAsync("ETH", default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Account.GetAccountInfoAsync(default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotExchangeData(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", default, default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT", default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetPriceAsync("ETHUSDT", default), false);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.ExchangeData.GetBookTickerAsync("ETHUSDT", default), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestSpotTrading(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.SpotApi.Trading.GetUserTradesAsync(default, default, default, default, default, default, default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestFuturesAccount(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetPositionModeAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetMultiAssetModeAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetBalancesAsync(default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetPositionMarginChangeHistoryAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetIncomeHistoryAsync(default, default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetLeverageBracketsAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetPositionAdlQuantileEstimationAsync(default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetUserCommissionRateAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Account.GetAccountInfoAsync(default, default), true);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestFuturesExchangeData(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetServerTimeAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetExchangeInfoAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT", default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT", default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT", default, default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay, default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetMarkPriceAsync("ETHUSDT", default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSDT", default, default, default, default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetFundingInfoAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetTickerAsync("ETHUSDT", default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetTickersAsync(default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetPriceAsync("ETHUSDT", default), false);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.ExchangeData.GetBookTickerAsync("ETHUSDT", default), false);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task TestFuturesTrading(bool newDeserialization)
        {
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Trading.GetPositionsAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Trading.GetForcedOrdersAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Trading.GetOpenOrdersAsync("ETHUSDT", default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Trading.GetOrdersAsync("ETHUSDT", default, default, default, default, default, default), true);
            await RunAndCheckResult(newDeserialization, client => client.FuturesApi.Trading.GetUserTradesAsync("ETHUSDT", default, default, default, default, default, default), true);
        }
    }
}
