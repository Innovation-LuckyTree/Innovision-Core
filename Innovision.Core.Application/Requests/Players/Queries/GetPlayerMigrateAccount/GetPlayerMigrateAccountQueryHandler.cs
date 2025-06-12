using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetPlayerMigrateAccount;

public class GetPlayerMigrateAccountQueryHandler : IRequestHandler<GetPlayerMigrateAccountQuery, PlayerMigrateAccountDto>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;

    public GetPlayerMigrateAccountQueryHandler(IMapper mapper, ICoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PlayerMigrateAccountDto> Handle(GetPlayerMigrateAccountQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.AccountObjectId == request.AccountObjectId && m.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .ProjectTo<PlayerMigrateAccountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = userInfo ?? throw new EntityNotFoundException("Account", request.AccountObjectId);

        return userInfo;
    }
}