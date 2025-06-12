using System.Text.Json.Serialization;

namespace Innovision.Core.Infrastructure.Games.Models.Responses;

public class CurrentBetSummary 
{
    [JsonPropertyName("total_user_count")]
    public int UserCount { get; set; }
    [JsonPropertyName("total_bet_transactions")]
    public int BetTransactionCount { get; set; }
    [JsonPropertyName("total_bet_amount")]
    public decimal TotalBetAmount { get; set; }
}
