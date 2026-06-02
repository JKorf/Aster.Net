using CryptoExchange.Net;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Authentication.Signing;
using Secp256k1Net;
using System;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster V3 credentials, allows for full functionality for the V3 Spot and Futures API
    /// </summary>
    public class AsterV3Credential : CredentialSet
    {
        private string? _publicAddress;
        private string? _privateKey;
        private string _privateSignerKey;

        /// <summary>
        /// Private key
        /// </summary>
        public string? PrivateKey
        {
            get => _privateKey;
            set
            {
                _privateKey = value;
                if (value != null)
                {
                    _publicAddress = GetPublicAddress(value);
                    Key = _publicAddress;
                }
                else
                {
                    _publicAddress = null;
                    Key = null!;
                }
            }
        }

        /// <summary>
        /// Public signer address key
        /// </summary>
        public string SignerKey { get; set; }

        /// <summary>
        /// Private signer key
        /// </summary>
        public string SignerPrivateKey
        {
            get => _privateSignerKey;
            set
            {
                _privateSignerKey = value;
                SignerKey = GetPublicAddress(value);
            }
        }

        /// <summary>
        /// Options binding constructor, not intended for direct use. Use the constructor with parameters or the ApiCredentials builder methods instead.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public AsterV3Credential() { }

        /// <summary>
        /// Aster V3 credentials, allows for full functionality for the V3 Spot and Futures API
        /// </summary>
        /// <param name="privateKey">Private key</param>
        /// <param name="signerPrivateKey">Signer private key</param>
        public AsterV3Credential(string privateKey, string signerPrivateKey): base(GetPublicAddress(privateKey))
        {
            PrivateKey = privateKey;
            SignerPrivateKey = signerPrivateKey;
        }

        /// <summary>
        /// Aster V3 credentials, allows for full functionality for the V3 Spot and Futures API
        /// </summary>
        /// <param name="publicAddress">Public address of the wallet to connect to Aster</param>
        /// <param name="signerPublicKey">Public API wallet/signer key</param>
        /// <param name="signerPrivateKey">Private API wallet/signer key</param>
        public AsterV3Credential(string publicAddress, string signerPublicKey, string signerPrivateKey) : base(publicAddress)
        {
            _publicAddress = publicAddress;
            SignerKey = signerPublicKey;
            SignerPrivateKey = signerPrivateKey;
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        /// <summary>
        /// Get the public address corresponding to the provided private key
        /// </summary>
        public string GetPublicAddress()
        {
            if (_publicAddress != null)
                return _publicAddress;

            _publicAddress = GetPublicAddress(PrivateKey!);
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

            return "0x" + ExchangeHelpers.BytesToHexString(pubAddress).ToLower();
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new AsterV3Credential(GetPublicAddress(), SignerKey, SignerPrivateKey)
        {
            PrivateKey = PrivateKey
        };

        /// <inheritdoc />
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrEmpty(SignerPrivateKey))
                throw new ArgumentException($"SignerPrivateKey not set on {GetType().Name}", nameof(SignerPrivateKey));
        }
    }
}
