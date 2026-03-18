using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster ECDsa credentials, used for the FuturesV3 API
    /// </summary>
    public class AsterFuturesCredential : CredentialPair
    {
        /// <summary>
        /// Signer key
        /// </summary>
        public string SignerKey { get; set; }
        /// <summary>
        /// Private key
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Aster ECDsa credentials, used for the FuturesV3 API
        /// </summary>
        /// <param name="key">Public address</param>
        /// <param name="signerKey">Signer key</param>
        /// <param name="privateKey">Private key</param>
        public AsterFuturesCredential(string key, string signerKey, string privateKey): base(key)
        {
            SignerKey = signerKey;
            PrivateKey = privateKey;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new AsterFuturesCredential(Key, SignerKey, PrivateKey);
    }
}
