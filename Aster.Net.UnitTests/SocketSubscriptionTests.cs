using Aster.Net.Clients;
using Aster.Net.Objects.Models;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aster.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateConcurrentFuturesSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new AsterSocketClient(Options.Create(new AsterSocketOptions
            {
                OutputOriginalData = true
            }), logger);

            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Futures", "wss://fstream.asterdex.com");
            await tester.ValidateConcurrentAsync<AsterKlineUpdate>(
                (client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler),
                (client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler),
                "Concurrent");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new AsterSocketClient(Options.Create(new AsterSocketOptions
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456"),
                OutputOriginalData = true
            }), logger);
            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Futures", "wss://fstream.asterdex.com");
            await tester.ValidateAsync<AsterAggregatedTradeUpdate>((client, handler) => client.FuturesApi.SubscribeToAggregatedTradeUpdatesAsync("ETHUSDT", handler), "Trades", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterMarkPriceUpdate>((client, handler) => client.FuturesApi.SubscribeToMarkPriceUpdatesAsync("ETHUSDT", null, handler), "MarkPrice", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterKlineUpdate>((client, handler) => client.FuturesApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler), "Klines", nestedJsonProperty: "data", ignoreProperties: ["B"]);
            await tester.ValidateAsync<AsterMiniTickUpdate>((client, handler) => client.FuturesApi.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", handler), "MiniTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterTickerUpdate>((client, handler) => client.FuturesApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterBookTickerUpdate>((client, handler) => client.FuturesApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterLiquidationUpdate>((client, handler) => client.FuturesApi.SubscribeToLiquidationUpdatesAsync("ETHUSDT", handler), "Liquidation", nestedJsonProperty: "data.o");
            await tester.ValidateAsync<AsterOrderBookUpdate>((client, handler) => client.FuturesApi.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", 5, 100, handler), "OrderBook", nestedJsonProperty: "data");

            await tester.ValidateAsync<AsterOrderUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync("123", onOrderUpdate: handler), "OrderUpdate", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterAccountUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync("123", onAccountUpdate: handler), "AccountUpdate", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterConfigUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync("123", onConfigUpdate: handler), "AccountConfig", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterConfigUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync("123", onConfigUpdate: handler), "AccountConfig2", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterMarginUpdate>((client, handler) => client.FuturesApi.SubscribeToUserDataUpdatesAsync("123", onMarginUpdate: handler), "AccountMargin", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateConcurrentSpotSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new AsterSocketClient(Options.Create(new AsterSocketOptions
            {
                OutputOriginalData = true
            }), logger);

            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Spot", "wss://sstream.asterdex.com");
            await tester.ValidateConcurrentAsync<AsterKlineUpdate>(
                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler),
                (client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler),
                "Concurrent");
        }

        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new AsterSocketClient(Options.Create(new AsterSocketOptions
            {
                ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456"),
                OutputOriginalData = true
            }), logger);
            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Spot", "wss://sstream.asterdex.com");
            await tester.ValidateAsync<AsterAggregatedTradeUpdate>((client, handler) => client.SpotApi.SubscribeToAggregatedTradeUpdatesAsync("ETHUSDT", handler), "Trades", nestedJsonProperty: "data", ignoreProperties: ["M"]);
            await tester.ValidateAsync<AsterKlineUpdate>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler), "Klines", nestedJsonProperty: "data", ignoreProperties: ["B"]);
            await tester.ValidateAsync<AsterMiniTickUpdate>((client, handler) => client.SpotApi.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", handler), "MiniTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterTickerUpdate>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterBookTickerUpdate>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterOrderBookUpdate>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", 5, 100, handler), "OrderBook", nestedJsonProperty: "data");

            await tester.ValidateAsync<AsterSpotOrderUpdate>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", onOrderUpdate: handler), "OrderUpdate", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterSpotAccountUpdate>((client, handler) => client.SpotApi.SubscribeToUserDataUpdatesAsync("123", onAccountUpdate: handler), "AccountUpdate", nestedJsonProperty: "data");
        }
    }
}
