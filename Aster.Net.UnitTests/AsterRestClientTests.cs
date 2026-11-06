using Aster.Net.Clients;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using CryptoExchange.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Testing;
using CryptoExchange.Net.Web3;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;

namespace Aster.Net.UnitTests
{
    [TestFixture()]
    public class AsterRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new AsterV3AuthenticationProvider(new AsterCredentials(new Objects.AsterV3Credential(
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"),
                    EthKey.FromPrivateKey("0x0000000000000000000000000000000000000000000000000000000000000001"))));
            var client = (RestApiClient)new AsterRestClient().FuturesV3Api;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return bodyParams["signature"].ToString();
                },
                "0xAA7B7E6835234560E32F03C565FD4697FF7B1C0D64C751D4969B741089F18F244FA5221AE814A528A45D3AB8E8392A94D125DF85ED0581C6165F3CA2FB9E28701B",
                new Parameters(AsterExchange._parameterSerializationSettings)
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827320559),
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<AsterRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<AsterSocketClient>();
        }

        [Test]
        public void TestSocketFuturesV3SharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new AsterSocketClient().FuturesV3Api.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestRestFuturesV3SharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new AsterRestClient().FuturesV3Api.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSocketSpotV3SharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new AsterSocketClient().SpotV3Api.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestRestSpotV3SharedApiDiscoveryMatchesAggregate()
        {
            var (missingOptions, missingInterfaces) = CryptoExchange.Net.Testing.TestHelpers.ValidateSharedApi(new AsterRestClient().SpotV3Api.SharedApi);

            Assert.That(missingOptions, Is.Empty);
            Assert.That(missingInterfaces, Is.Empty);
        }

        [Test]
        public void TestSpotRestSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new AsterRestClient().SpotV3Api.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestSpotSocketSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new AsterSocketClient().SpotV3Api.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestFuturesRestSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new AsterRestClient().FuturesV3Api.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }

        [Test]
        public void TestFuturesSocketSharedApiDoesntHaveUnsupportedCapabilities()
        {
            var unsupported = TestHelpers.ValidateUnsupportedCapabilities(new AsterSocketClient().FuturesV3Api.SharedApi);

            Assert.That(unsupported, Is.Empty);
        }
    }
}
