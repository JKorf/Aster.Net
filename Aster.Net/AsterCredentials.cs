using Aster.Net.Objects;
using CryptoExchange.Net.Authentication;
using System;
using System.Linq;

namespace Aster.Net
{
    /// <summary>
    /// Aster API credentials
    /// </summary>
    public class AsterCredentials : ApiCredentials
    {
        /// <summary>
        /// Provided credential type
        /// </summary>
        public ApiCredentialsType CredentialType => CredentialPairs.First(x => x is not AsterECDsaCredential).CredentialType;

        /// <summary>
        /// </summary>
        [Obsolete("Parameterless constructor is only for deserialization purposes and should not be used directly. Use parameterized constructor instead.")]
        public AsterCredentials() { }

        /// <summary>
        /// Create credentials using an API key and secret. HMAC authentication is assumed. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDsaCredential?)" /> instead.
        /// </summary>
        public AsterCredentials(string apiKey, string secretKey)
            : this(new HMACCredential(apiKey, secretKey)) { }

        /// <summary>
        /// Create credentials using HMAC credentials. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDsaCredential?)" /> instead.
        /// </summary>
        /// <param name="hmacCredential">HMAC credentials for the Spot and Futures API</param>
        public AsterCredentials(HMACCredential hmacCredential)
            : this(hmacCredential, null) 
        {
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Create credentials using RSA credentials in PEM/Base64 format. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDsaCredential?)" /> instead.
        /// </summary>
        /// <param name="rsaCredential">RSA credentials</param>
        public AsterCredentials(RSAPemCredential rsaCredential)
            : base(rsaCredential)
        {
        }
#endif
        /// <summary>
        /// Create credentials using RSA credentials in XML format. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDsaCredential?)" /> instead.
        /// </summary>
        /// <param name="rsaCredential">RSA credentials</param>
        public AsterCredentials(RSAXmlCredential rsaCredential)
            : base(rsaCredential)
        {
        }

        /// <summary>
        /// Create credentials using ECDsa credentials. This only grants access to the FuturesV3 API.If the Spot API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDsaCredential?)" /> instead.
        /// </summary>
        /// <param name="futuresV3Credential">ECDsa credentials for the FuturesV3 API</param>
        public AsterCredentials(AsterECDsaCredential futuresV3Credential)
            : base(futuresV3Credential)
        {
        }

        /// <summary>
        /// Create credentials proving both HMAC credentials for the Spot/Futures API's and ECDsa credentials for the FuturesV3 API
        /// </summary>
        /// <param name="hmacCredential">HMAC credentials for the Spot and Futures API</param>
        /// <param name="futuresV3Credential">ECDsa credentials for the FuturesV3 API</param>
        public AsterCredentials(HMACCredential? hmacCredential, AsterECDsaCredential? futuresV3Credential)
            : base(hmacCredential, futuresV3Credential)
        {
        }

        /// <summary>
        /// Create credentials proving both RSA credentials for the Spot/Futures API's and ECDsa credentials for the FuturesV3 API
        /// </summary>
        /// <param name="rsaCredential">RSA credentials for the Spot and Futures API</param>
        /// <param name="futuresV3Credential">ECDsa credentials for the FuturesV3 API</param>
        public AsterCredentials(RSACredential? rsaCredential, AsterECDsaCredential? futuresV3Credential)
            : base(rsaCredential, futuresV3Credential)
        {
        }

        /// <inheritdoc />
#pragma warning disable CS0618 // Type or member is obsolete
        public override ApiCredentials Copy() => new AsterCredentials { CredentialPairs = CredentialPairs };
#pragma warning restore CS0618 // Type or member is obsolete
    }
}
