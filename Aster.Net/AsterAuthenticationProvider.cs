using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Text;

namespace Aster.Net
{
    internal class AsterAuthenticationProvider : AuthenticationProvider<AsterCredentials>
    {
        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.Hmac, ApiCredentialsType.Rsa];
        public override string PublicKey => ApiCredentials.ApiKey;

        public AsterAuthenticationProvider(AsterCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-MBX-APIKEY", ApiCredentials.ApiKey);

            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var parameters = request.GetPositionParameters() ?? new Dictionary<string, object>();
            parameters.Add("timestamp", timestamp);

            if (request.ParameterPosition == HttpMethodParameterPosition.InUri)
            {
                var queryString = request.GetQueryString();
                var signature = Sign(queryString);
                parameters.Add("signature", signature);
                request.SetQueryString($"{queryString}&signature={signature}");
            }
            else
            {
                var parameterData = request.BodyParameters?.ToFormData() ?? string.Empty;
                var signature = Sign(parameterData);
                parameters.Add("signature", signature);
                request.SetBodyContent($"{parameterData}&signature={signature}");
            }
        }

        private string Sign(string data)
        {
            if (ApiCredentials.CredentialType == ApiCredentialsType.Hmac)
                return SignHMACSHA256(ApiCredentials.GetCredential<HMACCredential>()!, data);
            else
                return SignRSASHA256(ApiCredentials.GetCredential<RSACredential>()!, Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
        }
    }
}
