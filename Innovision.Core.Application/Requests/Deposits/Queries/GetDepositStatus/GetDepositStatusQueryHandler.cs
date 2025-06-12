using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Deposits.Queries.GetDepositStatus;

public class GetDepositStatusQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetDepositStatusQuery, DepositStatusVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    
    public async Task<DepositStatusVm> Handle(GetDepositStatusQuery request, CancellationToken cancellationToken)
    {
        var depositStatusList = await _coreDbContext.DepositStatuses
            .ProjectTo<DepositStatusDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new DepositStatusVm(depositStatusList);
    }
}
