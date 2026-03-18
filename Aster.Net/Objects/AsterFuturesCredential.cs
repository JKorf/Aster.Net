using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster ECDsa credentials, used for the FuturesV3 API
    /// </summary>
    public class AsterFuturesCredential : CredentialPair
    {
        /// <summary>
        /// Private key
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Public signer address key
        /// </summary>
        public string? SignerKey { get; set; }

        /// <summary>
        /// Private signer key
        /// </summary>
        public string? SignerPrivateKey { get; set; }

        /// <summary>
        /// Aster ECDsa credentials, used for the FuturesV3 API
        /// </summary>
        /// <param name="key">Public address</param>
        /// <param name="privateKey">Private key</param>
        /// <param name="signerKey">Signer key</param>
        /// <param name="signerPrivateKey">Signer private key key</param>
        public AsterFuturesCredential(string key, string privateKey, string? signerKey, string? signerPrivateKey): base(key)
        {
            PrivateKey = privateKey;
            SignerKey = signerKey;
            SignerPrivateKey = signerPrivateKey;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new AsterFuturesCredential(Key, PrivateKey, SignerKey, SignerPrivateKey);
    }
}
