using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerByAccountId;

public class GetPlayerByAccountIdQueryHandler(IMapper mapper, ICoreDbContext dbContext) : IRequestHandler<GetPlayerByAccountIdQuery, PlayerAccountDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;

    public async Task<PlayerAccountDto> Handle(GetPlayerByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.AccountInfoId == request.AccountId && m.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .ProjectTo<PlayerAccountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = userInfo ?? throw new EntityNotFoundException("Account", request.AccountId);

        return userInfo;
    }
}