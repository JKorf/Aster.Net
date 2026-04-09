using System;
using System.Text.Json.Serialization;
using Aster.Net.Enums;

namespace Aster.Net.Objects.Models;

/// <summary>
/// Withdrawal/deposit info
/// </summary>
public record AsterDepositWithdrawal
{
    /// <summary>
    /// ["<c>id</c>"] Id
    /// </summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }
    /// <summary>
    /// ["<c>type</c>"] Type
    /// </summary>
    [JsonPropertyName("type")]
    public DepositWithdrawType Type { get; set; }
    /// <summary>
    /// ["<c>asset</c>"] Asset
    /// </summary>
    [JsonPropertyName("asset")]
    public string Asset { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>amount</c>"] Quantity
    /// </summary>
    [JsonPropertyName("amount")]
    public decimal Quantity { get; set; }
    /// <summary>
    /// ["<c>state</c>"] Status
    /// </summary>
    [JsonPropertyName("state")]
    public DepositWithdrawStatus Status { get; set; }
    /// <summary>
    /// ["<c>txHash</c>"] Transaction hash
    /// </summary>
    [JsonPropertyName("txHash")]
    public string TransactionHash { get; set; } = string.Empty;
    /// <summary>
    /// ["<c>time</c>"] Timestamp
    /// </summary>
    [JsonPropertyName("time")]
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// ["<c>chainId</c>"] Network id
    /// </summary>
    [JsonPropertyName("chainId")]
    public long NetworkId { get; set; }
    /// <summary>
    /// ["<c>accountType</c>"] Account type
    /// </summary>
    [JsonPropertyName("accountType")]
    public AccountType AccountType { get; set; }
}

