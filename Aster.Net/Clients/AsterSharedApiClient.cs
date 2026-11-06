using Aster.Net.Interfaces.Clients;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotV3Api;
using Aster.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.Options;

namespace Aster.Net.Clients
{
    /// <inheritdoc />
    public class AsterSharedApiClient : SharedApiClientBase, IAsterSharedApiClient
    {
        /// <inheritdoc />
        public IAsterRestClientSpotV3SharedApi SpotV3Rest { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesV3SharedApi FuturesV3Rest { get; }
        /// <inheritdoc />
        public IAsterSocketClientSpotV3SharedApi SpotV3Socket { get; }
        /// <inheritdoc />
        public IAsterSocketClientFuturesV3SharedApi FuturesV3Socket { get; }

        /// <summary>
        /// ctor
        /// </summary>
        public AsterSharedApiClient(
            IAsterRestClient restClient,
            IAsterSocketClient socketClient,
            IOptions<AsterOptions> options)
            : base(options.Value.SharedApi.PreferredTransport,                  
                    [restClient.SpotV3Api.SharedApi,
                     restClient.FuturesV3Api.SharedApi,
                     socketClient.SpotV3Api.SharedApi,
                     socketClient.FuturesV3Api.SharedApi]
                  )
        {
            SpotV3Rest = restClient.SpotV3Api.SharedApi;
            FuturesV3Rest = restClient.FuturesV3Api.SharedApi;
            SpotV3Socket = socketClient.SpotV3Api.SharedApi;
            FuturesV3Socket = socketClient.FuturesV3Api.SharedApi;
        }
    }
}
