using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Aster.Net.Objects.Models;

/// <summary>
/// Withdraw info
/// </summary>
public record AsterWithdrawInfo
{
    /// <summary>
    /// ["<c>userDailyLimit</c>"] User daily limit in USD
    /// </summary>
    [JsonPropertyName("userDailyLimit")]
    public decimal UserDailyLimit { get; set; }
    /// <summary>
    /// ["<c>userRemainingDailyLimit</c>"] User remaining daily limit in USD
    /// </summary>
    [JsonPropertyName("userRemainingDailyLimit")]
    public decimal UserRemainingDailyLimit { get; set; }
    /// <summary>
    /// ["<c>totalDailyLimit</c>"] Total daily limit in USD
    /// </summary>
    [JsonPropertyName("totalDailyLimit")]
    public decimal TotalDailyLimit { get; set; }
    /// <summary>
    /// ["<c>totalRemainingDailyLimit</c>"] Total remaining daily limit in USD
    /// </summary>
    [JsonPropertyName("totalRemainingDailyLimit")]
    public decimal TotalRemainingDailyLimit { get; set; }
    /// <summary>
    /// ["<c>balances</c>"] Balances
    /// </summary>
    [JsonPropertyName("balances")]
    public Dictionary<string, AsterWithdrawInfoBalance> Balances { get; set; } = new()!;
} 

/// <summary>
/// Asset withdraw info
/// </summary>
public record AsterWithdrawInfoBalance
{
    /// <summary>
    /// ["<c>currency</c>"] Asset
    /// </summary>
    [JsonPropertyName("currency")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>spotTotalWithdrawAmount</c>"] Total spot balance available for withdrawal
    /// </summary>
    [JsonPropertyName("spotTotalWithdrawAmount")]
    public decimal SpotAvailableWithdrawQuantity { get; set; }
    /// <summary>
    /// ["<c>perpTotalWithdrawAmount</c>"] Total futures balance available for withdrawal
    /// </summary>
    [JsonPropertyName("perpTotalWithdrawAmount")]
    public decimal PerpAvailableWithdrawQuantity { get; set; }
    /// <summary>
    /// ["<c>dailyLimit</c>"] Daily limit in USD
    /// </summary>
    [JsonPropertyName("dailyLimit")]
    public decimal DailyLimit { get; set; }
    /// <summary>
    /// ["<c>chainBalances</c>"] Networks
    /// </summary>
    [JsonPropertyName("chainBalances")]
    public Dictionary<long, AsterWithdrawInfoNetwork> Networks { get; set; } = null!;
} 

/// <summary>
/// Network balance info
/// </summary>
public record AsterWithdrawInfoNetwork
{
    /// <summary>
    /// ["<c>chainId</c>"] Network id
    /// </summary>
    [JsonPropertyName("chainId")]
    public long NetworkId { get; set; }
    /// <summary>
    /// ["<c>spotMaxWithdrawAmount</c>"] Spot max withdraw quantity
    /// </summary>
    [JsonPropertyName("spotMaxWithdrawAmount")]
    public decimal SpotMaxWithdrawQuantity { get; set; }
    /// <summary>
    /// ["<c>perpMaxWithdrawAmount</c>"] Perp max withdraw quantity
    /// </summary>
    [JsonPropertyName("perpMaxWithdrawAmount")]
    public decimal PerpMaxWithdrawQuantity { get; set; }
    /// <summary>
    /// ["<c>chainLimit</c>"] Network limit
    /// </summary>
    [JsonPropertyName("chainLimit")]
    public decimal NetworkLimit { get; set; }
    /// <summary>
    /// ["<c>withdrawFee</c>"] Withdraw fee
    /// </summary>
    [JsonPropertyName("withdrawFee")]
    public decimal WithdrawFee { get; set; }
}

