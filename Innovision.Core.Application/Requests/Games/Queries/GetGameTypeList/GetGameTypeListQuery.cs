using MediatR;

namespace Innovision.Core.Application.Requests.Games.Queries.GetGameTypeList;

public record GetGameTypeListQuery(IEnumerable<int> GameTypes) : IRequest<GameTypesVm>;
