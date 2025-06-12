namespace Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;

public class OrderItemRequest
{
    public int GameTypeId { get; set; }
    public int BetItemType { get; set; }
    public string Values { get; set; }
    public decimal AmountBet { get; set; }
    public decimal ExcessAmount { get; set; }
    public bool BetException { get; set; } = false;
}