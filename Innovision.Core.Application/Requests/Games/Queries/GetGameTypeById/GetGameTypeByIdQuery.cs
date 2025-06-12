using MediatR;

namespace Innovision.Core.Application.Requests.Games.Queries.GetGameTypeById;

public record GetGameTypeByIdQuery(int GameTypeId) : IRequest<GameTypesDto>;
