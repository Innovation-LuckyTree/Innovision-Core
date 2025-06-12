using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetAccountInfoByUserId;

public class GetAccountInfoByUserIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAccountInfoByUserIdQuery, AccountInfoDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<AccountInfoDto> Handle(GetAccountInfoByUserIdQuery request, CancellationToken cancellationToken)
    {
        var accountInfo = await _coreDbContext.Accounts.Where(o => o.UserId == request.UserId)
            .ProjectTo<AccountInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return accountInfo;
    }
}