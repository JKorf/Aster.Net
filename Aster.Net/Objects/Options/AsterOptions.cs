using CryptoExchange.Net.Authentication;
using CryptoExchange.Net.Objects.Options;
using CryptoExchange.Net.SharedApis;

namespace Aster.Net.Objects.Options
{
    /// <summary>
    /// Aster options
    /// </summary>
    public class AsterOptions : LibraryOptions<AsterRestOptions, AsterSocketOptions, AsterCredentials, AsterEnvironment>
    {
        /// <summary>
        /// Options for Shared API usage
        /// </summary>
        public AsterSharedApiOptions SharedApi { get; set; } = new();
    }
}
