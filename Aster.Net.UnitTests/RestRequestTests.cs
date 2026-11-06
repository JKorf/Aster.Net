using Aster.Net.Clients;
using Aster.Net.Enums;
using Aster.Net.Objects;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using CryptoExchange.Net.Web3;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aster.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateFuturesV3AccountCalls()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());
            var client = new AsterRestClient(null, logger, Options.Create(new AsterRestOptions 
            {            
                AutoTimestamp = false,
                Environment = AsterEnvironment.CreateCustom("UnitTest", "https://sapi.asterdex.com/", "wss://localhost/", "https://fapi.asterdex.com/", "wss://localhost/"),
                ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001")))
            }));
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/FuturesV3/Account", "https://fapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetPositionModeAsync(), "GetPositionMode");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.SetPositionModeAsync(true), "SetPositionMode");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.SetMultiAssetModeAsync(true), "SetMultiAssetMode");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetMultiAssetModeAsync(), "GetMultiAssetMode");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.TransferAsync("USDT", Enums.TransferDirection.SpotToFutures, 1), "Transfer");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetAccountInfoAsync(), "GetAccountInfo");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.SetLeverageAsync("ETHUSDT", 1), "SetLeverage");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.SetMarginTypeAsync("ETHUSDT", Enums.MarginType.Isolated), "SetMarginType");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.ModifyIsolatedMarginAsync("ETHUSDT", Enums.MarginAdjustSide.Add, 1), "ModifyIsolatedMargin");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetPositionMarginChangeHistoryAsync("ETHUSDT"), "GetPositionMarginChangeHistory");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetIncomeHistoryAsync("ETHUSDT"), "GetIncomeHistory");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetLeverageBracketsAsync("ETHUSDT"), "GetLeverageBrackets");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetPositionAdlQuantileEstimationAsync(), "GetPositionAdlQuantileEstimation");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetUserCommissionRateAsync("ETHUSDT"), "GetUserCommissionRate");
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetWithdrawInfoAsync(), "GetWithdrawInfo", ignoreProperties: ["chainBalances"]);
            await tester.ValidateAsync(client => client.FuturesV3Api.Account.GetDepositWithdrawHistoryAsync(), "GetDepositWithdrawHistory");
        }

        [Test]
        public async Task ValidateFuturesV3ExchangeDataCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.Environment = AsterEnvironment.CreateCustom("UnitTest", "https://sapi.asterdex.com/", "wss://localhost/", "https://fapi.asterdex.com/", "wss://localhost/");
                opts.ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001")));
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/FuturesV3/ExchangeData", "https://fapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetIndexPriceKlines");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetMarkPriceAsync("ETHUSDT"), "GetMarkPrice");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetMarkPricesAsync(), "GetMarkPrices");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetFundingRatesAsync("ETHUSDT"), "GetFundingRates");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetFundingInfoAsync(), "GetFundingInfo");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker", ignoreProperties: ["prevClosePrice"]);
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetTickersAsync(), "GetTickers", ignoreProperties: ["prevClosePrice"]);
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetPricesAsync(), "GetPrices");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetBookTickerAsync("ETHUSDT"), "GetBookTicker");
            await tester.ValidateAsync(client => client.FuturesV3Api.ExchangeData.GetBookTickersAsync(), "GetBookTickers");
        }

        [Test]
        public async Task ValidateFuturesV3TradingCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.Environment = AsterEnvironment.CreateCustom("UnitTest", "https://sapi.asterdex.com/", "wss://localhost/", "https://fapi.asterdex.com/", "wss://localhost/");
                opts.ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001")));
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/FuturesV3/Trading", "https://fapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.OrderType.Market), "PlaceOrder");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.CancelAllOrdersAfterTimeoutAsync("ETHUSDT", TimeSpan.FromSeconds(5)), "CancelAllOrdersAfterTimeout");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetPositionsAsync("ETHUSDT"), "GetPositions");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetPositionsAsync("ETHUSDT"), "GetPositions2");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.GetForcedOrdersAsync("ETHUSDT"), "GetForcedOrders");
            await tester.ValidateAsync(client => client.FuturesV3Api.Trading.PlaceChaseOrderAsync("ETHUSDT", OrderSide.Buy, 0.1m, QuantityUnit.Base), "PlaceChaseOrder");
        }

        [Test]
        public async Task ValidateSpotV3AccountCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.Environment = AsterEnvironment.CreateCustom("UnitTest", "https://sapi.asterdex.com/", "wss://localhost/", "https://fapi.asterdex.com/", "wss://localhost/");
                opts.ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001")));
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/SpotV3/Account", "https://sapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotV3Api.Account.GetUserCommissionRateAsync("ETHUSDT"), "GetUserCommissionRate");
            await tester.ValidateAsync(client => client.SpotV3Api.Account.TransferAsync("ETH", Enums.TransferDirection.FuturesToSpot, 1), "Transfer");
            await tester.ValidateAsync(client => client.SpotV3Api.Account.GetAccountInfoAsync(), "GetAccountInfo");
        }

        [Test]
        public async Task ValidateSpotV3ExchangeDataCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/SpotV3/ExchangeData", "https://sapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades", ignoreProperties: ["baseQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory", ignoreProperties: ["baseQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory");
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker", ignoreProperties: ["prevClosePrice"]);
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice");
            await tester.ValidateAsync(client => client.SpotV3Api.ExchangeData.GetBookTickerAsync("ETHUSDT"), "GetBookTicker");
        }

        [Test]
        public async Task ValidateSpotV3TradingCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.Environment = AsterEnvironment.CreateCustom("UnitTest", "https://sapi.asterdex.com/", "wss://localhost/", "https://fapi.asterdex.com/", "wss://localhost/");
                opts.ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001")));
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/SpotV3/Trading", "https://sapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.OrderType.TakeProfit, 1), "PlaceOrder", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotV3Api.Trading.GetUserTradesAsync(), "GetUserTrades", ignoreProperties: ["counterpartyId", "createUpdateId"]);
        }

        private bool IsAuthenticated(IHttpResult result)
        {
            return result.RequestUrl?.Contains("signature") == true || result.RequestBody?.Contains("signature=") == true;
        }
    }
}
