using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Authentication.Signing;
using Secp256k1Net;
using System;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster V3 credentials, used for the V3 Spot and Futures API
    /// </summary>
    public class AsterV3Credential : CredentialPair
    {
        private string? _publicAddress;

        /// <summary>
        /// Private key
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Public signer address key
        /// </summary>
        public string SignerKey { get; set; }

        /// <summary>
        /// Private signer key
        /// </summary>
        public string SignerPrivateKey { get; set; }

        /// <summary>
        /// Aster V3 credentials, used for the V3 Spot and Futures API
        /// </summary>
        /// <param name="userPrivateKey">Private key</param>
        /// <param name="signerPrivateKey">Signer private key</param>
        public AsterV3Credential(string userPrivateKey, string signerPrivateKey): base(GetPublicAddress(userPrivateKey))
        {
            PrivateKey = userPrivateKey;
            SignerKey = GetPublicAddress(signerPrivateKey);
            SignerPrivateKey = signerPrivateKey;
        }

        /// <summary>
        /// Get the public address corresponding to the provided private key
        /// </summary>
        public string GetPublicAddress()
        {
            if (_publicAddress != null)
                return _publicAddress;

            _publicAddress = GetPublicAddress(PrivateKey);
            return _publicAddress;
        }

        private static string GetPublicAddress(string privateKey)
        {
            var publicKeyBytes = Secp256k1.CreatePublicKey(ExchangeHelpers.HexToBytesString(privateKey), false);

            var withoutPrefix = new byte[64];
            Array.Copy(publicKeyBytes, 1, withoutPrefix, 0, 64);

            var hash = CeSha3Keccack.CalculateHash(withoutPrefix);
            var pubAddress = new byte[20];
            Array.Copy(hash, hash.Length - 20, pubAddress, 0, 20);

            return "0x" + ExchangeHelpers.BytesToHexString(pubAddress);
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new AsterV3Credential(PrivateKey, SignerPrivateKey);

        /// <inheritdoc />
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(PrivateKey))
                throw new ArgumentException("PrivateKey unset", nameof(PrivateKey));

            if (string.IsNullOrEmpty(SignerPrivateKey))
                throw new ArgumentException("SignerPrivateKey unset", nameof(SignerPrivateKey));
        }
    }
}
