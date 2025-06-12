using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerInformationList;

public class GetPlayerInformationListQueryHandler(IMapper mapper, ICoreDbContext dbContext) : IRequestHandler<GetPlayerInformationListQuery, PlayerAccountVm>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;

    public async Task<PlayerAccountVm> Handle(GetPlayerInformationListQuery request, CancellationToken cancellationToken)
    {
        var userInfoList = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => request.AccountIds.Contains(m.AccountInfoId) && m.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .ProjectTo<PlayerAccountDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PlayerAccountVm(userInfoList);
    }
}