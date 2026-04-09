using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Account type
/// </summary>
[JsonConverter(typeof(EnumConverter<AccountType>))]
public enum AccountType
{
    /// <summary>
    /// ["<c>spot</c>"] Spot account
    /// </summary>
    [Map("spot")]
    Spot,
    /// <summary>
    /// ["<c>perp</c>"] Futures account
    /// </summary>
    [Map("perp")]
    Perp,
}
