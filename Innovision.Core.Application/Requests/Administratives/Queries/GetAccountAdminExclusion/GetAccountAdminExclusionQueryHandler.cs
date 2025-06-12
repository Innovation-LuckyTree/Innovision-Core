using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountAdminExclusion;

public class GetAccountAdminExclusionQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAccountAdminExclusionQuery, AdministrativeExclusionDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<AdministrativeExclusionDto> Handle(GetAccountAdminExclusionQuery request, CancellationToken cancellationToken)
    {
        var adminExclusion = await _coreDbContext.AdministrativeExclusions
            .Where(o => o.AccountId == request.AccountId && o.DateExpiry >= DateTime.UtcNow && o.Status == 1)
            .ProjectTo<AdministrativeExclusionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return adminExclusion;
    }
}

