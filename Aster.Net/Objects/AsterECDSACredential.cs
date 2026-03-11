using CryptoExchange.Net.Authentication;

namespace Aster.Net.Objects
{
    public class AsterECDSACredential : CredentialPair
    {
        public string PublicKey { get; set; }
        public string SignerKey { get; set; }
        public string PrivateKey { get; set; }

        public override ApiCredentialsType CredentialType => ApiCredentialsType.Ecdsa;
        public override string PublicIdentifier => PublicKey;

        public AsterECDSACredential(string publicKey, string signerKey, string privateKey)
        {
            PublicKey = publicKey;
            SignerKey = signerKey;
            PrivateKey = privateKey;
        }
    }
}
