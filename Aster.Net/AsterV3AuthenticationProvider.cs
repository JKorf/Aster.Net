using Aster.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Authentication.Signing;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Secp256k1Net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aster.Net
{
    internal class AsterV3AuthenticationProvider : AuthenticationProvider<AsterCredentials, AsterV3Credential>
    {
        private static IEnumerable<(string Name, string Type, object Value)> GetDomainFields(
            string action,
            string version,
            int chainId,
            string verifyingAddress)
        {
            return [
                ("name", "string", action),
                ("version", "string", version),
                ("chainId", "uint256", chainId),
                ("verifyingContract", "address", verifyingAddress),
                ];
        }

        private static readonly Dictionary<Type, string> _typeMapping = new Dictionary<Type, string>
        {
            { typeof(string), "string" },
            { typeof(long), "uint256" },
            { typeof(bool), "bool" }
        };


        public AsterV3AuthenticationProvider(AsterCredentials credentials) : base(credentials, credentials.V3)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            request.Headers ??= new Dictionary<string, string>();

            var nonce = GetMillisecondTimestampLong(apiClient) * 1000;
            var parameters = request.GetPositionParameters();

            string signatureHex;
            string paramString;
            if (parameters.ContainsKey("signaction"))
            {
                if (Credential.PrivateKey == null)
                    throw new ArgumentException("Endpoint requires full credentials with private key");

                var signAction = (string)parameters["signaction"];
                parameters.Remove("signaction");

                parameters["asterChain"] = "Mainnet";
                parameters["user"] = ApiCredentials.V3!.Key;
                parameters["nonce"] = nonce;

                var domainFields = GetDomainFields("AsterSignTransaction", "1", 56, "0x0000000000000000000000000000000000000000");
                var messageFields = GetMessageFields(parameters);
                var message = CeEip712TypedDataEncoder.EncodeEip721(signAction, domainFields, messageFields);
                var keccakSigned = CeSha3Keccack.CalculateHash(message);
                signatureHex = SignRequest(keccakSigned, Credential.PrivateKey!).ToLower();

                parameters["signatureChainId"] = 56;
                paramString = request.GetPositionParameters().CreateParamString(false, request.ArraySerialization);
            }
            else
            {
                parameters["nonce"] = nonce;
                parameters["user"] = Credential.Key;
                parameters["signer"] = Credential.SignerKey;

                var chainId = apiClient.EnvironmentName == TradeEnvironmentNames.Live ? AsterExchange._mainnetChainId : AsterExchange._testnetChainId;

                paramString = request.GetPositionParameters().CreateParamString(false, request.ArraySerialization);
            
                var typedData = GetTradingTypedData(paramString, chainId);
                var message = CeEip712TypedDataEncoder.EncodeTypedDataRaw(typedData);
                var keccakSigned = CeSha3Keccack.CalculateHash(message);
                signatureHex = SignRequest(keccakSigned, Credential.SignerPrivateKey).ToLower();
                parameters["signature"] = signatureHex;
            }

            if (request.ParameterPosition == HttpMethodParameterPosition.InBody)
                request.SetBodyContent(paramString + "&signature=" + signatureHex);
            else
                request.SetQueryString(paramString + "&signature=" + signatureHex);
        }


        private List<(string Name, string Type, object Value)> GetMessageFields(IDictionary<string, object> parameters)
        {
            var result = new List<(string, string, object)>();

            foreach (var parameter in parameters)
            {
                if (parameter.Value.ToString() == "true" || parameter.Value.ToString() == "false")
                {
                    result.Add(
                            (parameter.Key.Substring(0, 1).ToUpperInvariant() + parameter.Key.Substring(1),
                            "bool",
                            bool.Parse((string)parameter.Value)));
                }
                else 
                {
                    result.Add(
                        (parameter.Key.Substring(0, 1).ToUpperInvariant() + parameter.Key.Substring(1),
                        _typeMapping[parameter.Value.GetType()],
                        parameter.Value));
                }
            }

            return result;
        }

        private CeTypedDataRaw GetTradingTypedData(string parameterString, uint chainId)
        {
            return new CeTypedDataRaw
            {
                PrimaryType = "Message",
                DomainRawValues = new CeMemberValue[]
                {
                    new CeMemberValue { TypeName = "string", Value = "AsterSignTransaction" },
                    new CeMemberValue { TypeName = "string", Value = "1" },
                    new CeMemberValue { TypeName = "uint256", Value = chainId },
                    new CeMemberValue { TypeName = "address", Value = "0x0000000000000000000000000000000000000000" }
                },
                Message = new CeMemberValue[]
                {
                    new CeMemberValue { TypeName = "string", Value = parameterString },
                },
                Types = new Dictionary<string, CeMemberDescription[]>
                {
                    { "EIP712Domain",
                        new CeMemberDescription[]
                        {
                            new CeMemberDescription { Name = "name", Type = "string" },
                            new CeMemberDescription { Name = "version", Type = "string" },
                            new CeMemberDescription { Name = "chainId", Type = "uint256" },
                            new CeMemberDescription { Name = "verifyingContract", Type = "address" }
                        }
                    },
                    { "Message",
                        new CeMemberDescription[]
                        {
                            new CeMemberDescription { Name = "msg", Type = "string" },
                        }
                    }
                }
            };
        }

        public static string SignRequest(byte[] request, string secret)
        {
            (var signature, var recover) = Secp256k1.SignRecoverable(request, HexToBytesString(secret));
            var hexCompactR = BytesToHexString(new ArraySegment<byte>(signature, 0, 32));
            var hexCompactS = BytesToHexString(new ArraySegment<byte>(signature, 32, 32));
            var hexCompactV = recover + 27;
            return $"{hexCompactR}{hexCompactS}{recover:X2}";
        }
    }
}
