using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;

public record GetGameProviderListByCategoryIdQuery(int GameCategoryId) : IRequest<GameProviderVm>;
