using MediatR;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;

public record GetGameProviderListQuery(int CategoryId = 0, bool IsFavorites = false) : IRequest<GameProviderVm>;
