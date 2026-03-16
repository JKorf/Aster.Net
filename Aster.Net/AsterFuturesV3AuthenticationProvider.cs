using Aster.Net.Objects;
using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Authentication.Signing;
using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects;
using Secp256k1Net;
using System;
using System.Collections.Generic;

namespace Aster.Net
{
    internal class AsterFuturesV3AuthenticationProvider : AuthenticationProvider<AsterCredentials, AsterECDsaCredential>
    {
        public override ApiCredentialsType[] SupportedCredentialTypes => [ApiCredentialsType.ECDsa];

        public AsterFuturesV3AuthenticationProvider(AsterCredentials credentials) : base(credentials)
        {
        }

        public override void ProcessRequest(RestApiClient apiClient, RestRequestConfiguration request)
        {
            if (!request.Authenticated)
                return;

            request.Headers ??= new Dictionary<string, string>();

            var nonce = GetMillisecondTimestampLong(apiClient) * 1000;
            var parameters = request.GetPositionParameters();
            parameters["nonce"] = nonce.ToString();
            parameters["user"] = Credential.Key;
            parameters["signer"] = Credential.SignerKey;

            var paramString = request.GetPositionParameters().CreateParamString(false, request.ArraySerialization);
            
            var typedData = GetTypedData(paramString, 1666);
            var message = CeEip712TypedDataEncoder.EncodeTypedDataRaw(typedData);
            var keccakSigned = CeSha3Keccack.CalculateHash(message);
            var signatureHex = SignRequest(keccakSigned, Credential.PrivateKey);
            signatureHex = signatureHex.ToLower();

            parameters["signature"] = signatureHex;

            if (request.ParameterPosition == HttpMethodParameterPosition.InBody)
                request.SetBodyContent(paramString + "&signature=" + signatureHex);
            else
                request.SetQueryString(paramString + "&signature=" + signatureHex);
        }


        private CeTypedDataRaw GetTypedData(string parameterString, uint chainId)
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
