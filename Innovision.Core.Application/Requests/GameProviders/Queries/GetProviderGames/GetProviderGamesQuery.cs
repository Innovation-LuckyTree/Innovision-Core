using MediatR;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;

public record GetProviderGamesQuery(int GameCategoryId, int GameProviderId) : IRequest<GameProviderVm>;
