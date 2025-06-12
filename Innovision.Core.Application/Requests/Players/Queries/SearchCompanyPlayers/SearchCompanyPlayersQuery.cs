using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerByAccountId;

public record SearchCompanyPlayersQuery(Guid? CompanyId) : IRequest<PlayerAccountVm>;

public class SearchCompanyPlayersQueryHandler : IRequestHandler<SearchCompanyPlayersQuery, PlayerAccountVm>
{
    private readonly ICoreDbContext _coreDbContext;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SearchCompanyPlayersQueryHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator)
    {
        _coreDbContext = coreDbContext;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<PlayerAccountVm> Handle(SearchCompanyPlayersQuery request, CancellationToken cancellationToken)
    {
        var players = await _coreDbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .ProjectTo<PlayerAccountDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PlayerAccountVm(players);
    }
}