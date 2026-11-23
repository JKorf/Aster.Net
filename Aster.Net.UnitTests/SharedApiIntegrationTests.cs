using Aster.Net.Interfaces.Clients;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using CryptoExchange.Net.SharedApis;
using CryptoExchange.Net.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Aster.Net.UnitTests
{
    [TestFixture]
    public class AsterSharedApiRestIntegrationTest : SharedApiRestIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync() => await ExecuteAsync();

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddAster();
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IAsterSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedRest client, CapabilityOptions options)
        {
            if (options.SupportedTradingModes.Contains(TradingMode.Spot))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");

            return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");
        }
    }

    [TestFixture]
    public class AsterSharedApiSocketIntegrationTest : SharedApiSocketIntegrationTest
    {
        protected override bool ManualRun => false;

        [Test]
        public override async Task RunAsync()
        {
            var result = await ExecuteAsync();
            foreach (var item in result)
                Assert.Warn(item.Message);
        }

        protected override ISharedApiClientBase GetClient()
        {
            var collection = new ServiceCollection();
            collection.AddAster();
            var provider = collection.BuildServiceProvider();
            return provider.GetRequiredService<IAsterSharedApiClient>();
        }

        protected override SharedApiTestConfig GetConfig(ISharedSocket client, CapabilityOptions options)
        {
            if (options.SupportedTradingModes.Contains(TradingMode.Spot))
                return new SharedApiTestConfig(new SharedSymbol(TradingMode.Spot, "ETH", "USDT"), "ETH");

            return new SharedApiTestConfig(new SharedSymbol(TradingMode.PerpetualLinear, "ETH", "USDT"), "ETH");
        }
    }
}
