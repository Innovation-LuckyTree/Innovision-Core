using MediatR;

namespace Innovision.Core.Application.Requests.GameProviders.Queries;
public record GetGameProviderListQuery : IRequest<GameProviderVm>
{

}
