using CryptoExchange.Net.Attributes;
using CryptoExchange.Net.Converters.SystemTextJson;
using System.Text.Json.Serialization;

namespace Aster.Net.Enums;

/// <summary>
/// Deposit or withdraw type
/// </summary>
[JsonConverter(typeof(EnumConverter<DepositWithdrawType>))]
public enum DepositWithdrawType
{
    /// <summary>
    /// ["<c>DEPOSIT</c>"] Deposit
    /// </summary>
    [Map("DEPOSIT")]
    Deposit,
    /// <summary>
    /// ["<c>WITHDRAW</c>"] Withdrawal
    /// </summary>
    [Map("WITHDRAW")]
    Withdrawal,
}
