using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByUsername;

public class GetAccountByUsernameQueryHandler : IRequestHandler<GetAccountByUsernameQuery, AccountDto>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;

    public GetAccountByUsernameQueryHandler(IMapper mapper, ICoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<AccountDto> Handle(GetAccountByUsernameQuery request, CancellationToken cancellationToken)
    {
        var accountQuery = _dbContext.Accounts
                .Where(o => (o.MobileNumber == request.Username || o.Email == request.Username) && o.IsActive);

        if (request.IsPlayer)
            accountQuery = accountQuery.Where(o => o.UserTypeId == Domain.Enums.UserTypes.Player);
        else
            accountQuery = accountQuery.Where(o => o.UserTypeId != Domain.Enums.UserTypes.Player);

        var account = await accountQuery.ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return account;
    }
}