using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    public class AsterECDSACredential : CredentialPair
    {
        public string PublicKey { get; set; }
        public string SignerKey { get; set; }
        public string PrivateKey { get; set; }

        public override ApiCredentialsType CredentialType => ApiCredentialsType.Ecdsa;

        public AsterECDSACredential(string publicKey, string signerKey, string privateKey): base(publicKey)
        {
            PublicKey = publicKey;
            SignerKey = signerKey;
            PrivateKey = privateKey;
        }
    }
}
