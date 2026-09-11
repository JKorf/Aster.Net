using Aster.Net.Interfaces.Clients;
using Aster.Net.Interfaces.Clients.FuturesApi;
using Aster.Net.Interfaces.Clients.FuturesV3Api;
using Aster.Net.Interfaces.Clients.SpotApi;
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
        public IAsterRestClientSpotSharedApi SpotRest { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesSharedApi FuturesRest { get; }
        /// <inheritdoc />
        public IAsterRestClientSpotV3SharedApi SpotV3Rest { get; }
        /// <inheritdoc />
        public IAsterRestClientFuturesV3SharedApi FuturesV3Rest { get; }
        /// <inheritdoc />
        public IAsterSocketClientSpotSharedApi SpotSocket { get; }
        /// <inheritdoc />
        public IAsterSocketClientFuturesSharedApi FuturesSocket { get; }
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
                  options.Value.SharedApi.ApiVersion == AsterApiVersion.V1 ?
                    [restClient.SpotApi.SharedApi,
                     restClient.FuturesApi.SharedApi,
                     socketClient.SpotApi.SharedApi,
                     socketClient.FuturesApi.SharedApi] :
                    [restClient.SpotV3Api.SharedApi,
                     restClient.FuturesV3Api.SharedApi,
                     socketClient.SpotV3Api.SharedApi,
                     socketClient.FuturesV3Api.SharedApi]
                  )
        {
            SpotRest = restClient.SpotApi.SharedApi;
            FuturesRest = restClient.FuturesApi.SharedApi;
            SpotV3Rest = restClient.SpotV3Api.SharedApi;
            FuturesV3Rest = restClient.FuturesV3Api.SharedApi;
            SpotSocket = socketClient.SpotApi.SharedApi;
            FuturesSocket = socketClient.FuturesApi.SharedApi;
            SpotV3Socket = socketClient.SpotV3Api.SharedApi;
            FuturesV3Socket = socketClient.FuturesV3Api.SharedApi;
        }
    }
}
