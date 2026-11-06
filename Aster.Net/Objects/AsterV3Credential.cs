using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Web3;
using System;

namespace Aster.Net.Objects
{
    /// <summary>
    /// Aster V3 credentials, allows for full functionality for the V3 Spot and Futures API
    /// </summary>
    public class AsterV3Credential : CredentialSet
    {
        /// <summary>
        /// Private key
        /// </summary>
        public EthKey Wallet { get; }
        /// <summary>
        /// Signer wallet info
        /// </summary>
        public EthKey Signer { get; }

        /// <summary>
        /// Aster V3 credentials, allows for full functionality for the V3 Spot and Futures API
        /// </summary>
        /// <param name="wallet">The spender wallet</param>
        /// <param name="signer">The signer wallet</param>
        public AsterV3Credential(EthKey wallet, EthKey signer): base(wallet.PublicAddress)
        {
            Wallet = wallet;
            Signer = signer;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy() => new AsterV3Credential(Wallet, Signer);


        /// <inheritdoc />
        public override void Validate()
        {
            base.Validate();

            if (Signer?.PrivateKey == null)
                throw new ArgumentException($"Signer private key not set on {GetType().Name}", nameof(Signer));
        }
    }
}
