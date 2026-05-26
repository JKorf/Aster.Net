using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aster.Net.Enums
{

    /// <summary>
    /// Strategy type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<StrategyType>))]
    public enum StrategyType
    {
        /// <summary>
        /// One triggers other
        /// </summary>
        [Map("OCO")]
        Oco,
        /// <summary>
        /// One triggers other
        /// </summary>
        [Map("OTO")]
        Oto,
        /// <summary>
        /// One triggers and cancels other
        /// </summary>
        [Map("OTOCO")]
        Otoco,
        /// <summary>
        /// Chase order
        /// </summary>
        [Map("CHASE")]
        Chase
    }
}
