using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountByMobileNumber;

public class GetAccountByMobileNumberQueryHandler : IRequestHandler<GetAccountByMobileNumberQuery, AccountDto>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;

    public GetAccountByMobileNumberQueryHandler(IMapper mapper, ICoreDbContext dbContext)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<AccountDto> Handle(GetAccountByMobileNumberQuery request, CancellationToken cancellationToken)
    {
        var accountQuery = _dbContext.Accounts
                .Where(o => o.MobileNumber == request.MobileNumber && o.IsActive);

        var account = await accountQuery.ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return account;
    }
}