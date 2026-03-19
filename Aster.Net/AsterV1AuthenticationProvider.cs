using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aster.Net
{
    internal class AsterV1AuthenticationProvider : AuthenticationProvider<AsterCredentials>
    {
        public override string Key => ApiCredentials.V1!.Key;

        public AsterV1AuthenticationProvider(AsterCredentials credentials) : base(credentials)
        {
            if (credentials.V1 == null)
                throw new ArgumentException("V1 credentials not provided", nameof(credentials));
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            request.Headers ??= new Dictionary<string, string>();
            request.Headers.Add("X-MBX-APIKEY", ApiCredentials.V1!.Key);

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
            if (ApiCredentials.V1 is HMACCredential hmacCred)
                return SignHMACSHA256(hmacCred, data);
            else if (ApiCredentials.V1 is RSACredential rsaCred)
                return SignRSASHA256(rsaCred, Encoding.ASCII.GetBytes(data), SignOutputType.Base64);
            else
                throw new NotImplementedException();
        }
    }
}
