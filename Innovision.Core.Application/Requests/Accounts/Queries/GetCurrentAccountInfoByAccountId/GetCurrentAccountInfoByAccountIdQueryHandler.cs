using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;

public class GetCurrentAccountInfoByAccountIdQueryHandler(IMapper mapper, ICoreDbContext dbContext) : IRequestHandler<GetCurrentAccountInfoByAccountIdQuery, AccountInfoDto>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;

    public async Task<AccountInfoDto> Handle(GetCurrentAccountInfoByAccountIdQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.AccountObjectId == request.AccountObjectId && UserContants.ACCOUNT_TYPES_WITH_WALLET.Contains(m.UserTypeId))
            .ProjectTo<AccountInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
            
        _ = userInfo ?? throw new EntityNotFoundException("Account", request.AccountObjectId);

        return userInfo;
    }
}
