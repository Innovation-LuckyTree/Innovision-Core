namespace Innovision.Core.Application.Requests.Orders.Commands.AddItemOrder;

public record OrderItemVm(long OrderId, string TransactionNo, IEnumerable<long> OrderItems, decimal Amount);