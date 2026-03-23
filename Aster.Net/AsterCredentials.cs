using Aster.Net.Objects;
using CryptoExchange.Net.Authentication;
using System;

namespace Aster.Net
{
    /// <summary>
    /// Aster API credentials
    /// </summary>
    public class AsterCredentials : ApiCredentials
    {
        internal CredentialSet? V1 { get; set; }
        /// <summary>
        /// V3 API credentials
        /// </summary>
        public AsterV3Credential? V3 { get; set; }

        /// <summary>
        /// Create new credentials
        /// </summary>
        public AsterCredentials() { }

        /// <summary>
        /// Create new credentials providing V1 HMAC credentials. To access the V3 API provide AsterV3Credential credential using a different overload
        /// </summary>
        /// <param name="key">V1 API key</param>
        /// <param name="secret">V1 API secret</param>
        public AsterCredentials(string key, string secret)
        {
            V1 = new HMACCredential(key, secret);
        }

        /// <summary>
        /// Create new credentials providing V1 credentials in HMAC format. To access the V3 API provide AsterV3Credential credential using a different overload
        /// </summary>
        /// <param name="v1HmacCredential">V1 API HMAC credentials</param>
        public AsterCredentials(HMACCredential v1HmacCredential)
        {
            V1 = v1HmacCredential;
        }

        /// <summary>
        /// Create new credentials providing V1 credentials in RSA XML format. To access the V3 API provide AsterV3Credential credential using a different overload
        /// </summary>
        /// <param name="v1RsaXmlCredential">V1 API RSA XML credentials</param>
        public AsterCredentials(RSAXmlCredential v1RsaXmlCredential)
        {
            V1 = v1RsaXmlCredential;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Create new credentials providing V1 credentials in RSA PEM/Base64 format. To access the V3 API provide AsterV3Credential credential using a different overload
        /// </summary>
        /// <param name="v1RsaPemCredential">V1 API RSA PEM/Base64 credentials</param>
        public AsterCredentials(RSAPemCredential v1RsaPemCredential)
        {
            V1 = v1RsaPemCredential;
        }
#endif

        /// <summary>
        /// Create new credentials providing V3 credential
        /// </summary>
        /// <param name="v3Credential">V3 API credential</param>
        public AsterCredentials(AsterV3Credential v3Credential)
        {
            V3 = v3Credential;
        }

        /// <summary>
        /// V1 API credentials in HMAC format
        /// </summary>
        public HMACCredential? V1HMAC
        {
            get => V1 as HMACCredential;
            set { if (value != null) V1 = value; }
        }

        /// <summary>
        /// V1 API credentials in RSA XML format
        /// </summary>
        public RSAXmlCredential? V1RSAXml
        {
            get => V1 as RSAXmlCredential;
            set { if (value != null) V1 = value; }
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// V1 API credentials in RSA PEM/Base64 format
        /// </summary>
        public RSAPemCredential? V1RSAPem
        {
            get => V1 as RSAPemCredential;
            set { if (value != null) V1 = value; }
        }
#endif

        /// <summary>
        /// V1 API credentials in HMAC format. To access the V3 API provide AsterV3Credential credential using the WithV3 method
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public AsterCredentials WithV1HMAC(string key, string secret)
        {
            if (V1 != null) throw new InvalidOperationException("V1 credentials already set");

            V1 = new HMACCredential(key, secret);
            return this;
        }

        /// <summary>
        /// V1 API credentials in RSA XML format. To access the V3 API provide AsterV3Credential credential using the WithV3 method
        /// </summary>
        /// <param name="key">Public key</param>
        /// <param name="privateKey">Private key</param>
        public AsterCredentials WithV1RSAXml(string key, string privateKey)
        {
            if (V1 != null) throw new InvalidOperationException("V1 credentials already set");

            V1 = new RSAXmlCredential(key, privateKey);
            return this;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// V1 API credentials in RSA PEM/Base64 format. To access the V3 API provide AsterV3Credential credential using the WithV3 method
        /// </summary>
        /// <param name="key">Public key</param>
        /// <param name="privateKey">Private key</param>
        public AsterCredentials WithV1RSAPem(string key, string privateKey)
        {
            if (V1 != null) throw new InvalidOperationException("V1 credentials already set");

            V1 = new RSAPemCredential(key, privateKey);
            return this;
        }
#endif

        /// <summary>
        /// V3 API credentials
        /// </summary>
        /// <param name="privateKey">Private key. The private key of the user address connecting to Aster</param>
        /// <param name="signerPrivateKey">Signer private key. The private key generated when authorizing a new API wallet in the [Api management] - [Pro API] UI</param>
        public AsterCredentials WithV3(string privateKey, string signerPrivateKey)
        {
            if (V3 != null) throw new InvalidOperationException("V3 credentials already set");

            V3 = new AsterV3Credential(privateKey, signerPrivateKey);
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
        {
            return new AsterCredentials
            {
                V3 = V3,
                V1 = V1
            };
        }

        /// <inheritdoc />
        public override void Validate() 
        {
            V1?.Validate();
            V3?.Validate();
        }
    }
}
