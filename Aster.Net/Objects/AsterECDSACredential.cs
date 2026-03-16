using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster ECDsa credentials, used for the FuturesV3 API
    /// </summary>
    public class AsterECDsaCredential : CredentialPair
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
        /// Credential type
        /// </summary>
        public override ApiCredentialsType CredentialType => ApiCredentialsType.ECDsa;

        /// <summary>
        /// Aster ECDsa credentials, used for the FuturesV3 API
        /// </summary>
        /// <param name="key">Public address</param>
        /// <param name="signerKey">Signer key</param>
        /// <param name="privateKey">Private key</param>
        public AsterECDsaCredential(string key, string signerKey, string privateKey): base(key)
        {
            SignerKey = signerKey;
            PrivateKey = privateKey;
        }
    }
}
