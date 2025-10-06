using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Aster.Net
{
    internal class AsterAuthenticationProvider : AuthenticationProvider
    {
        public AsterAuthenticationProvider(ApiCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers.Add("X-MBX-APIKEY", ApiKey);

            if (!request.Authenticated)
                return;

            var timestamp = GetMillisecondTimestamp(apiClient);
            var parameters = request.GetPositionParameters();
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
                var parameterData = request.BodyParameters.ToFormData();
                var signature = Sign(parameterData);
                parameters.Add("signature", signature);
                request.SetBodyContent($"{parameterData}&signature={signature}");
            }
        }

        private string Sign(string data)
        {
            if (_credentials.CredentialType == ApiCredentialsType.Hmac)
                return SignHMACSHA256(data);
            else
                return SignRSASHA256(Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
        }
    }
}
