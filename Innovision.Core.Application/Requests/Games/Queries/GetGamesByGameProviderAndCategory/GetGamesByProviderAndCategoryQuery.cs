using Innovision.Core.Common.Models;
using MediatR;

namespace Innovision.Core.Application.Requests.Games.Queries;

public record GetGamesByProviderAndCategoryQuery(int GameCategoryId, int GameProviderId, PagedQuery? Query) : IRequest<GameVm>;
