using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountSelfLimit;

public class GetAccountLimitQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAccountLimitQuery, AccountLimitVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<AccountLimitVm> Handle(GetAccountLimitQuery request, CancellationToken cancellationToken)
    {
        var selfLimit = await _coreDbContext.SelfLimits.Where(o => o.AccountId == request.AccountId && o.Status == 1)
            .ProjectTo<SelfLimitDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        var adminExclusion = await _coreDbContext.AdministrativeExclusions
            .Where(o => o.AccountId == request.AccountId && o.DateExpiry >= DateTime.UtcNow && o.Status == 1)
            .ProjectTo<AdministrativeExclusionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return new AccountLimitVm(adminExclusion, selfLimit);
    }
}