using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAccountSelfLimit;

public class GetAccountSelfLimitQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAccountSelfLimitQuery, SelfLimitDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SelfLimitDto> Handle(GetAccountSelfLimitQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.SelfLimits.Where(o => o.AccountId == request.AccountId && o.Status == 1)
            .ProjectTo<SelfLimitDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return result;
    }
}
