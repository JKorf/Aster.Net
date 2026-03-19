using Aster.Net.Objects;
using CryptoExchange.Net.Authentication;
using System;
using System.Linq;
using System.Net;

namespace Aster.Net
{
    /// <summary>
    /// Aster API credentials
    /// </summary>
    public class AsterCredentials : ApiCredentials
    {
        internal CredentialPair? Spot { get; set; }
        internal AsterFuturesCredential? FuturesV3 { get; set; }

        /// <summary>
        /// Create new credentials
        /// </summary>
        public AsterCredentials() { }

        /// <summary>
        /// Create new credentials providing Spot HMAC credentials
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public AsterCredentials(string key, string secret)
        {
            Spot = new HMACCredential(key, secret);
        }

        /// <summary>
        /// Create new credentials providing only Spot credentials in HMAC format
        /// </summary>
        /// <param name="spotHmacCredential">HMAC credentials</param>
        public AsterCredentials(HMACCredential spotHmacCredential)
        {
            Spot = spotHmacCredential;
        }

        /// <summary>
        /// Create new credentials providing only Spot credentials in RSA XML format
        /// </summary>
        /// <param name="rsaXmlCredential">RSA XML credentials</param>
        public AsterCredentials(RSAXmlCredential rsaXmlCredential)
        {
            Spot = rsaXmlCredential;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Create new credentials providing only Spot credentials in RSA PEM/Base64 format
        /// </summary>
        /// <param name="rsaPemCredential">RSA PEM/Base64 credentials</param>
        public AsterCredentials(RSAPemCredential rsaPemCredential)
        {
            Spot = rsaPemCredential;
        }
#endif

        /// <summary>
        /// Create new credentials providing only Futures V3 credentials
        /// </summary>
        /// <param name="futuresCredential">Futures V3 credentials</param>
        public AsterCredentials(AsterFuturesCredential futuresCredential)
        {
            FuturesV3 = futuresCredential;
        }

        /// <summary>
        /// Create new credentials providing Spot credentials in HMAC format and Futures credentials
        /// </summary>
        /// <param name="spotHmacCredential">Spot HMAC credentials</param>
        /// <param name="futuresV3Credential">Futures V3 credentials</param>
        public AsterCredentials(HMACCredential spotHmacCredential, AsterFuturesCredential futuresV3Credential)
        {
            Spot = spotHmacCredential;
            FuturesV3 = futuresV3Credential;
        }

        /// <summary>
        /// Create new credentials providing Spot credentials in RSA XML format and Futures credentials
        /// </summary>
        /// <param name="rsaXmlCredential">Spot RSA XML credentials</param>
        /// <param name="futuresV3Credential">Futures V3 credentials</param>
        public AsterCredentials(RSAXmlCredential rsaXmlCredential, AsterFuturesCredential futuresV3Credential)
        {
            Spot = rsaXmlCredential;
            FuturesV3 = futuresV3Credential;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Create new credentials providing Spot credentials in RSA PEM format and Futures V3 credentials
        /// </summary>
        /// <param name="futuresV3Credential">Futures V3 credentials</param>
        /// <param name="rsaPemCredential">Spot RSA PEM credentials</param>
        public AsterCredentials(RSAPemCredential rsaPemCredential, AsterFuturesCredential futuresV3Credential)
        {
            Spot = rsaPemCredential;
            FuturesV3 = futuresV3Credential;
        }
#endif

        /// <summary>
        /// Spot credentials in HMAC format
        /// </summary>
        public HMACCredential? SpotHMAC
        {
            get => Spot as HMACCredential;
            set { if (value != null) Spot = value; }
        }

        /// <summary>
        /// Spot credentials in RSA XML format
        /// </summary>
        public RSAXmlCredential? SpotRSAXml
        {
            get => Spot as RSAXmlCredential;
            set { if (value != null) Spot = value; }
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Spot credentials in RSA PEM/Base64 format
        /// </summary>
        public RSAPemCredential? SpotRSAPem
        {
            get => Spot as RSAPemCredential;
            set { if (value != null) Spot = value; }
        }
#endif

        /// <summary>
        /// Spot credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public AsterCredentials WithSpotHMAC(string key, string secret)
        {
            if (Spot != null) throw new InvalidOperationException("Spot credentials already set");

            Spot = new HMACCredential(key, secret);
            return this;
        }

        /// <summary>
        /// Spot credentials in RSA XML format
        /// </summary>
        /// <param name="key">Public key</param>
        /// <param name="privateKey">Private key</param>
        public AsterCredentials WithSpotRSAXml(string key, string privateKey)
        {
            if (Spot != null) throw new InvalidOperationException("Spot credentials already set");

            Spot = new RSAXmlCredential(key, privateKey);
            return this;
        }

#if NETSTANDARD2_1_OR_GREATER || NET7_0_OR_GREATER
        /// <summary>
        /// Spot credentials in RSA PEM/Base64 format
        /// </summary>
        /// <param name="key">Public key</param>
        /// <param name="privateKey">Private key</param>
        public AsterCredentials WithSpotRSAPem(string key, string privateKey)
        {
            if (Spot != null) throw new InvalidOperationException("Spot credentials already set");

            Spot = new RSAPemCredential(key, privateKey);
            return this;
        }
#endif

        /// <summary>
        /// Futures V3 credentials
        /// </summary>
        /// <param name="key">Public key</param>
        /// <param name="privateKey">Private key</param>
        /// <param name="signerKey">Signer public key</param>
        /// <param name="signerPrivateKey">Signer private key</param>
        public AsterCredentials WithFuturesV3(string key, string privateKey, string? signerKey = null, string? signerPrivateKey = null)
        {
            if (FuturesV3 != null) throw new InvalidOperationException("Futures credentials already set");

            FuturesV3 = new AsterFuturesCredential(key, privateKey, signerKey, signerPrivateKey);
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
        {
            return new AsterCredentials
            {
                FuturesV3 = FuturesV3,
                Spot = Spot
            };
        }

        /// <inheritdoc />
        public override void Validate() 
        {
            Spot?.Validate();
            FuturesV3?.Validate();
        }
    }
}
