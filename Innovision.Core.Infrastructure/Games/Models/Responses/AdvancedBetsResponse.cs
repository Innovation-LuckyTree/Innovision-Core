namespace Innovision.Core.Infrastructure.Games.Models.Responses;

public class AdvancedBetsResponse
{
    public  DateTimeOffset Date { get; set; }
    public decimal TotalAmount { get; set; }
    public IEnumerable<int> OrderItemIds { get; set; }
}
