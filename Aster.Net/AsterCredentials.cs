using Aster.Net.Objects;
using CryptoExchange.Net.Authentication;
using System.Linq;

namespace Aster.Net
{
    /// <summary>
    /// Aster API credentials
    /// </summary>
    public class AsterCredentials : ApiCredentials
    {
        public ApiCredentialsType CredentialType => CredentialPairs.First(x => x is not AsterECDSACredential).CredentialType;
        public string ApiKey => CredentialPairs.First(x => x is not AsterECDSACredential).PublicKey;

        /// <summary>
        /// Create credentials using an API key and secret. HMAC authentication is assumed. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        public AsterCredentials(string apiKey, string secretKey)
            : this(new HMACCredential(apiKey, secretKey)) { }

        /// <summary>
        /// Create credentials using HMAC credentials. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        /// <param name="hmacCredential">HMAC credentials for the Spot and Futures API</param>
        public AsterCredentials(HMACCredential hmacCredential)
            : this(hmacCredential, null) 
        {
        }

        /// <summary>
        /// Create credentials using RSA credentials. If the FuturesV3 API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
        /// </summary>
        /// <param name="rsaCredential">RSA credentials for the Spot and Futures API</param>
        public AsterCredentials(RSACredential rsaCredential)
            : this(rsaCredential, null)
        {
        }

        /// <summary>
        /// Create credentials using ECDSA credentials. This only grants access to the FuturesV3 API.If the Spot API will be used use <see cref="AsterCredentials(HMACCredential?, AsterECDSACredential?)" /> instead.
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
        }

        /// <summary>
        /// Create credentials proving both RSA credentials for the Spot/Futures API's and ECDSA credentials for the FuturesV3 API
        /// </summary>
        /// <param name="rsaCredential">RSA credentials for the Spot and Futures API</param>
        /// <param name="futuresV3Credential">ECDSA credentials for the FuturesV3 API</param>
        public AsterCredentials(RSACredential? rsaCredential, AsterECDSACredential? futuresV3Credential)
            : base(rsaCredential, futuresV3Credential)
        {
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => 
            Hmac == null 
                ? new AsterCredentials(GetCredential<RSACredential>(), GetCredential<AsterECDSACredential>())
                : new AsterCredentials(GetCredential<HMACCredential>(), GetCredential<AsterECDSACredential>());
    }
}
