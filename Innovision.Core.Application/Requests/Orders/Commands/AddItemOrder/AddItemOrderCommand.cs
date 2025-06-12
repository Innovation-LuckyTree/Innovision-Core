using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;

public class AddItemOrderCommand : IRequest<OrderItemVm>
{
    public int GameId { get; set; }
    public IEnumerable<OrderItemRequest> OrderItems { get; set; }
    public int TotalItems { get; set; } = 1;
    public decimal TotalAmount { get; set; }
    public bool UseBet { get; set; }
    public bool? IsBonus { get; set; } = false;
}
