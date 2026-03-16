using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster ECDSA credentials, used for the FuturesV3 API
    /// </summary>
    public class AsterECDSACredential : CredentialPair
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
        public override ApiCredentialsType CredentialType => ApiCredentialsType.Ecdsa;

        /// <summary>
        /// Aster ECDSA credentials, used for the FuturesV3 API
        /// </summary>
        /// <param name="key">Public address</param>
        /// <param name="signerKey">Signer key</param>
        /// <param name="privateKey">Private key</param>
        public AsterECDSACredential(string key, string signerKey, string privateKey): base(key)
        {
            SignerKey = signerKey;
            PrivateKey = privateKey;
        }
    }
}
