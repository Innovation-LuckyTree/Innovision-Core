using MediatR;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetailsByOrder;

public record GetJackpotDetailsByOrderQuery(IEnumerable<long> OrderItemIds) : IRequest<JackpotDetailVm>;
