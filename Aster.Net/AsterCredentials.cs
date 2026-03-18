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
        internal AsterFuturesCredential? Futures { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public AsterCredentials() { }

        /// <summary>
        /// Create new credentials providing only spot credentials in HMAC format
        /// </summary>
        /// <param name="key">API key</param>
        /// <param name="secret">API secret</param>
        public AsterCredentials(string key, string secret)
        {
            Spot = new HMACCredential(key, secret);
        }

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
        /// Futures credentials
        /// </summary>
        /// <param name="key">Public key</param>
        /// <param name="privateKey">Private key</param>
        /// <param name="signerKey">Signer public key</param>
        /// <param name="signerPrivateKey">Signer private key</param>
        public AsterCredentials WithFutures(string key, string privateKey, string? signerKey = null, string? signerPrivateKey = null)
        {
            if (Futures != null) throw new InvalidOperationException("Futures credentials already set");

            Futures = new AsterFuturesCredential(key, privateKey, signerKey, signerPrivateKey);
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
        {
            return new AsterCredentials
            {
                Futures = Futures,
                Spot = Spot
            };
        }
    }
}
