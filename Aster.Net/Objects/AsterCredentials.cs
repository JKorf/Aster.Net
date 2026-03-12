using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster API credentials
    /// </summary>
    public class AsterCredentials : ApiCredentials
    {
        public ApiCredentialsType CredentialType { get; set; }
        public string ApiKey => GetCredential<HMACCredential>()?.Key ?? GetCredential<RSACredential>()?.PublicKey;
        public string ApiSecret => GetCredential<HMACCredential>()?.Secret ?? GetCredential<RSACredential>()?.PrivateKey;

        /// <summary>
        /// Create credentials using an API key and secret. HMAC authentication is assumed. If the FuturesV3 API will be used use <see cref="AsterCredentials.AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        public AsterCredentials(string apiKey, string secretKey)
            : this(new HMACCredential(apiKey, secretKey)) { }

        /// <summary>
        /// Create credentials using HMAC credentials. If the FuturesV3 API will be used use <see cref="AsterCredentials.AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        /// <param name="hmacCredential">HMAC credentials for the Spot and Futures API</param>
        public AsterCredentials(HMACCredential hmacCredential)
            : this(hmacCredential, null) 
        {
            CredentialType = ApiCredentialsType.Hmac;
        }

        /// <summary>
        /// Create credentials using RSA credentials. If the FuturesV3 API will be used use <see cref="AsterCredentials.AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        /// <param name="rsaCredential">RSA credentials for the Spot and Futures API</param>
        public AsterCredentials(RSACredential rsaCredential)
            : this(rsaCredential, null)
        {
            CredentialType = ApiCredentialsType.Rsa;
        }

        /// <summary>
        /// Create credentials using ECDSA credentials. This only grants access to the FuturesV3 API.If the Spot API will be used use <see cref="AsterCredentials.AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        /// <param name="futuresV3Credential">ECDSA credentials for the FuturesV3 API</param>
        public AsterCredentials(AsterECDSACredential futuresV3Credential)
            : base(futuresV3Credential)
        {
        }

        /// <summary>
        /// Create credentials proving both HMAC credentials for the Spot/Futures API's and ECDSA credentials for the FuturesV3 API
        /// </summary>
        /// <param name="hmacCredential">HMAC credentials for the Spot and Futures API</param>
        /// <param name="futuresV3Credential">ECDSA credentials for the FuturesV3 API</param>
        public AsterCredentials(HMACCredential? hmacCredential, AsterECDSACredential? futuresV3Credential)
            : base(hmacCredential, futuresV3Credential)
        {
            CredentialType = ApiCredentialsType.Hmac;
        }

        /// <summary>
        /// Create credentials proving both RSA credentials for the Spot/Futures API's and ECDSA credentials for the FuturesV3 API
        /// </summary>
        /// <param name="rsaCredential">RSA credentials for the Spot and Futures API</param>
        /// <param name="futuresV3Credential">ECDSA credentials for the FuturesV3 API</param>
        public AsterCredentials(RSACredential? rsaCredential, AsterECDSACredential? futuresV3Credential)
            : base(rsaCredential, futuresV3Credential)
        {
            CredentialType = ApiCredentialsType.Rsa;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => 
            Hmac == null 
                ? new AsterCredentials(GetCredential<RSACredential>(), GetCredential<AsterECDSACredential>())
                : new AsterCredentials(GetCredential<HMACCredential>(), GetCredential<AsterECDSACredential>());
    }
}
