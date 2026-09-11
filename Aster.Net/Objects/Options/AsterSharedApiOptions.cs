using CryptoExchange.Net.SharedApis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aster.Net.Objects.Options
{
    /// <inheritdoc />
    public class AsterSharedApiOptions : SharedApiOptions
    {
        /// <summary>
        /// Which version of the API to use for shared API calls.
        /// </summary>
        public AsterApiVersion ApiVersion { get; set; } = AsterApiVersion.V3;
    }

    /// <summary>
    /// API version
    /// </summary>
    public enum AsterApiVersion
    {
        /// <summary>
        /// V1 API, requires V1 credentials to be set when using private endpoints
        /// </summary>
        V1,
        /// <summary>
        /// V3 API, requires V3 credentials to be set when using private endpoints
        /// </summary>
        V3
    }
}
