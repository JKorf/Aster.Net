using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Deposit/withdrawal status
/// </summary>
[JsonConverter(typeof(EnumConverter<DepositWithdrawStatus>))]
public enum DepositWithdrawStatus
{
    /// <summary>
    /// ["<c>SUCCESS</c>"] Success
    /// </summary>
    [Map("SUCCESS")]
    Success,
    /// <summary>
    /// ["<c>PROCESSING</c>"] Processing
    /// </summary>
    [Map("PROCESSING")]
    Processing,
    /// <summary>
    /// ["<c>FAILED</c>"] Failed
    /// </summary>
    [Map("FAILED")]
    Failed,
}
