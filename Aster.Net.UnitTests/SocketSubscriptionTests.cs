using Aster.Net.Clients;
using Aster.Net.Objects.Models;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using CryptoExchange.Net.Web3;
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

            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Futures", "wss://fstream.asterdex.com/stream");
            await tester.ValidateConcurrentAsync<AsterKlineUpdate>(
                (client, handler) => client.FuturesV3Api.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler),
                (client, handler) => client.FuturesV3Api.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler),
                "Concurrent");
        }

        [Test]
        public async Task ValidateFuturesSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new AsterSocketClient(Options.Create(new AsterSocketOptions
            {
                ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"))),
                OutputOriginalData = true
            }), logger);
            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Futures", "wss://fstream.asterdex.com");
            await tester.ValidateAsync<AsterAggregatedTradeUpdate>((client, handler) => client.FuturesV3Api.SubscribeToAggregatedTradeUpdatesAsync("ETHUSDT", handler), "Trades", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterMarkPriceUpdate>((client, handler) => client.FuturesV3Api.SubscribeToMarkPriceUpdatesAsync("ETHUSDT", null, handler), "MarkPrice", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterKlineUpdate>((client, handler) => client.FuturesV3Api.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler), "Klines", nestedJsonProperty: "data", ignoreProperties: ["B"]);
            await tester.ValidateAsync<AsterMiniTickUpdate>((client, handler) => client.FuturesV3Api.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", handler), "MiniTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterTickerUpdate>((client, handler) => client.FuturesV3Api.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterBookTickerUpdate>((client, handler) => client.FuturesV3Api.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterLiquidationUpdate>((client, handler) => client.FuturesV3Api.SubscribeToLiquidationUpdatesAsync("ETHUSDT", handler), "Liquidation", nestedJsonProperty: "data.o");
            await tester.ValidateAsync<AsterOrderBookUpdate>((client, handler) => client.FuturesV3Api.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", 5, 100, handler), "OrderBook", nestedJsonProperty: "data");

            await tester.ValidateAsync<AsterOrderUpdate>((client, handler) => client.FuturesV3Api.SubscribeToUserDataUpdatesAsync("123", onOrderUpdate: handler), "OrderUpdate", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterAccountUpdate>((client, handler) => client.FuturesV3Api.SubscribeToUserDataUpdatesAsync("123", onAccountUpdate: handler), "AccountUpdate", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterConfigUpdate>((client, handler) => client.FuturesV3Api.SubscribeToUserDataUpdatesAsync("123", onConfigUpdate: handler), "AccountConfig", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterConfigUpdate>((client, handler) => client.FuturesV3Api.SubscribeToUserDataUpdatesAsync("123", onConfigUpdate: handler), "AccountConfig2", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterMarginUpdate>((client, handler) => client.FuturesV3Api.SubscribeToUserDataUpdatesAsync("123", onMarginUpdate: handler), "AccountMargin", nestedJsonProperty: "data");
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

            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Spot", "wss://sstream.asterdex.com/stream");
            await tester.ValidateConcurrentAsync<AsterKlineUpdate>(
                (client, handler) => client.SpotV3Api.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler),
                (client, handler) => client.SpotV3Api.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler),
                "Concurrent");
        }

        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var logger = new LoggerFactory();
            logger.AddProvider(new TraceLoggerProvider());

            var client = new AsterSocketClient(Options.Create(new AsterSocketOptions
            {
                ApiCredentials = new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"))),
                OutputOriginalData = true
            }), logger);
            var tester = new SocketSubscriptionValidator<AsterSocketClient>(client, "Subscriptions/Spot", "wss://sstream.asterdex.com");
            await tester.ValidateAsync<AsterAggregatedTradeUpdate>((client, handler) => client.SpotV3Api.SubscribeToAggregatedTradeUpdatesAsync("ETHUSDT", handler), "Trades", nestedJsonProperty: "data", ignoreProperties: ["M"]);
            await tester.ValidateAsync<AsterKlineUpdate>((client, handler) => client.SpotV3Api.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneHour, handler), "Klines", nestedJsonProperty: "data", ignoreProperties: ["B"]);
            await tester.ValidateAsync<AsterMiniTickUpdate>((client, handler) => client.SpotV3Api.SubscribeToMiniTickerUpdatesAsync("ETHUSDT", handler), "MiniTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterTickerUpdate>((client, handler) => client.SpotV3Api.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterBookTickerUpdate>((client, handler) => client.SpotV3Api.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterOrderBookUpdate>((client, handler) => client.SpotV3Api.SubscribeToPartialOrderBookUpdatesAsync("ETHUSDT", 5, 100, handler), "OrderBook", nestedJsonProperty: "data");

            await tester.ValidateAsync<AsterSpotOrderUpdate>((client, handler) => client.SpotV3Api.SubscribeToUserDataUpdatesAsync("123", onOrderUpdate: handler), "OrderUpdate", nestedJsonProperty: "data");
            await tester.ValidateAsync<AsterSpotAccountUpdate>((client, handler) => client.SpotV3Api.SubscribeToUserDataUpdatesAsync("123", onAccountUpdate: handler), "AccountUpdate", nestedJsonProperty: "data");
        }
    }
}
