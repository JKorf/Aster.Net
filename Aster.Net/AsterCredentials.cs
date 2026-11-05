using Aster.Net.Objects;
using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Web3;
using System;

namespace Aster.Net
{
    /// <summary>
    /// Aster API credentials
    /// </summary>
    public class AsterCredentials : ApiCredentials
    {
        /// <summary>
        /// V3 API credentials
        /// </summary>
        public AsterV3Credential? V3 { get; set; }

        /// <summary>
        /// Create new credentials
        /// </summary>
        public AsterCredentials() { }

        /// <summary>
        /// Create new credentials providing V3 credential
        /// </summary>
        /// <param name="v3Credential">V3 API credential</param>
        public AsterCredentials(AsterV3Credential v3Credential)
        {
            V3 = v3Credential;
        }

        /// <summary>
        /// V3 API credentials
        /// </summary>
        /// <param name="wallet">Spending wallet. The key of the user address connecting to Aster</param>
        /// <param name="signer">Signer wallet.</param>
        public AsterCredentials WithV3(EthKey wallet, EthKey signer)
        {
            if (V3 != null) throw new InvalidOperationException("V3 credentials already set");

            V3 = new AsterV3Credential(wallet, signer);
            return this;
        }

        /// <inheritdoc />
        public override ApiCredentials Copy()
        {
            return new AsterCredentials
            {
                V3 = V3
            };
        }

        /// <inheritdoc />
        public override void Validate() 
        {
            V3?.Validate();
        }
    }
}
