using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aster.Net.Clients;

namespace Aster.Net.UnitTests
{
    [TestFixture]
    public class RestRequestTests
    {
        [Test]
        public async Task ValidateSpotAccountCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/Spot/Account", "https://sapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Account.GetUserCommissionRateAsync("ETHUSDT"), "GetUserCommissionRate");
            await tester.ValidateAsync(client => client.SpotApi.Account.TransferAsync("ETH", Enums.TransferDirection.FuturesToSpot, 1), "Transfer");
            await tester.ValidateAsync(client => client.SpotApi.Account.SendToAddressAsync("ETH", "123", 1), "SendToAddress");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetWithdrawFeeAsync("ETH", Enums.NetworkType.Ethereum), "GetWithdrawFee");
            await tester.ValidateAsync(client => client.SpotApi.Account.GetAccountInfoAsync(), "GetAccountInfo");
        }

        [Test]
        public async Task ValidateSpotExchangeDataCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/Spot/ExchangeData", "https://sapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades", ignoreProperties: ["baseQty"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory", ignoreProperties: ["baseQty"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetAggregatedTradeHistoryAsync("ETHUSDT"), "GetAggregatedTradeHistory");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker", ignoreProperties: ["prevClosePrice"]);
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice");
            await tester.ValidateAsync(client => client.SpotApi.ExchangeData.GetBookTickerAsync("ETHUSDT"), "GetBookTicker");
        }

        [Test]
        public async Task ValidateSpotTradingCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/Spot/Trading", "https://sapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.SpotApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Sell, Enums.OrderType.TakeProfit, 1), "PlaceOrder", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders", ignoreProperties: ["cumQty"]);
            await tester.ValidateAsync(client => client.SpotApi.Trading.GetUserTradesAsync(), "GetUserTrades", ignoreProperties: ["counterpartyId", "createUpdateId"]);
        }

        [Test]
        public async Task ValidateFuturesAccountCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/Futures/Account", "https://fapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionModeAsync(), "GetPositionMode");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetPositionModeAsync(true), "SetPositionMode");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetMultiAssetModeAsync(true), "SetMultiAssetMode");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetMultiAssetModeAsync(), "GetMultiAssetMode");
            await tester.ValidateAsync(client => client.FuturesApi.Account.TransferAsync("USDT", Enums.TransferDirection.SpotToFutures, 1), "Transfer");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetBalancesAsync(), "GetBalances");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetAccountInfoAsync(), "GetAccountInfo");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetLeverageAsync("ETHUSDT", 1), "SetLeverage");
            await tester.ValidateAsync(client => client.FuturesApi.Account.SetMarginTypeAsync("ETHUSDT", Enums.MarginType.Isolated), "SetMarginType");
            await tester.ValidateAsync(client => client.FuturesApi.Account.ModifyIsolatedMarginAsync("ETHUSDT", Enums.MarginAdjustSide.Add, 1), "ModifyIsolatedMargin");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionMarginChangeHistoryAsync("ETHUSDT"), "GetPositionMarginChangeHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetIncomeHistoryAsync("ETHUSDT"), "GetIncomeHistory");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetLeverageBracketsAsync("ETHUSDT"), "GetLeverageBrackets");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetPositionAdlQuantileEstimationAsync(), "GetPositionAdlQuantileEstimation");
            await tester.ValidateAsync(client => client.FuturesApi.Account.GetUserCommissionRateAsync("ETHUSDT"), "GetUserCommissionRate");
        }

        [Test]
        public async Task ValidateFuturesExchangeDataCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/Futures/ExchangeData", "https://fapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetExchangeInfoAsync(), "GetExchangeInfo");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetOrderBookAsync("ETHUSDT"), "GetOrderBook");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetRecentTradesAsync("ETHUSDT"), "GetRecentTrades");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT"), "GetTradeHistory");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetIndexPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetIndexPriceKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetMarkPriceKlinesAsync("ETHUSDT", Enums.KlineInterval.OneDay), "GetMarkPriceKlines");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetMarkPriceAsync("ETHUSDT"), "GetMarkPrice");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetMarkPricesAsync(), "GetMarkPrices");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingRatesAsync("ETHUSDT"), "GetFundingRates");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetFundingInfoAsync(), "GetFundingInfo");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickerAsync("ETHUSDT"), "GetTicker", ignoreProperties: ["prevClosePrice"]);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetTickersAsync(), "GetTickers", ignoreProperties: ["prevClosePrice"]);
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetPriceAsync("ETHUSDT"), "GetPrice");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetPricesAsync(), "GetPrices");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetBookTickerAsync("ETHUSDT"), "GetBookTicker");
            await tester.ValidateAsync(client => client.FuturesApi.ExchangeData.GetBookTickersAsync(), "GetBookTickers");
        }

        [Test]
        public async Task ValidateFuturesTradingCalls()
        {
            var client = new AsterRestClient(opts =>
            {
                opts.AutoTimestamp = false;
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new RestRequestValidator<AsterRestClient>(client, "Endpoints/Futures/Trading", "https://fapi.asterdex.com", IsAuthenticated);
            await tester.ValidateAsync(client => client.FuturesApi.Trading.PlaceOrderAsync("ETHUSDT", Enums.OrderSide.Buy, Enums.OrderType.Market), "PlaceOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrderAsync("ETHUSDT", 123), "GetOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelOrderAsync("ETHUSDT", 123), "CancelOrder");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOpenOrdersAsync("ETHUSDT"), "GetOpenOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetOrdersAsync("ETHUSDT"), "GetOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelAllOrdersAsync("ETHUSDT"), "CancelAllOrders");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.CancelAllOrdersAfterTimeoutAsync("ETHUSDT", TimeSpan.FromSeconds(5)), "CancelAllOrdersAfterTimeout");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionsAsync("ETHUSDT"), "GetPositions");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetPositionsAsync("ETHUSDT"), "GetPositions2");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetUserTradesAsync("ETHUSDT"), "GetUserTrades");
            await tester.ValidateAsync(client => client.FuturesApi.Trading.GetForcedOrdersAsync("ETHUSDT"), "GetForcedOrders");
        }

        private bool IsAuthenticated(WebCallResult result)
        {
            return result.RequestUrl?.Contains("signature") == true || result.RequestBody?.Contains("signature=") == true;
        }
    }
}
