using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.SystemTextJson;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using Aster.Net.Clients;

namespace Aster.Net.UnitTests
{
    [TestFixture()]
    public class AsterRestClientTests
    {
        [Test]
        public void CheckSignatureExample1()
        {
            var authProvider = new AsterAuthenticationProvider(new ApiCredentials("XXX", "XXX"));
            var client = (RestApiClient)new AsterRestClient().FuturesApi;

            CryptoExchange.Net.Testing.TestHelpers.CheckSignature(
                client,
                authProvider,
                HttpMethod.Post,
                "/api/v3/order",
                (uriParams, bodyParams, headers) =>
                {
                    return bodyParams["signature"].ToString();
                },
                "D8931A9A64A70323E272938E34E56EA63FF665AA44AC8A9A7AA4BDFF74614816",
                new Dictionary<string, object>
                {
                    { "symbol", "LTCBTC" },
                },
                DateTimeConverter.ParseFromDouble(1499827320559),
                true,
                false);
        }

        [Test]
        public void CheckInterfaces()
        {
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingRestInterfaces<AsterRestClient>();
            CryptoExchange.Net.Testing.TestHelpers.CheckForMissingSocketInterfaces<AsterSocketClient>();
        }
    }
}
