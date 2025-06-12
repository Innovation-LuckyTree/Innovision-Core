using MediatR;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerInformationList;

public record GetPlayerInformationListQuery(IEnumerable<long> AccountIds) : IRequest<PlayerAccountVm>;
