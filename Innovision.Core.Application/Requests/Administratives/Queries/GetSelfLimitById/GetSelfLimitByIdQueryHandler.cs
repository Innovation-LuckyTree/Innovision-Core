using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetSelfLimitById;

public class GetSelfLimitByIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetSelfLimitByIdQuery, SelfLimitDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<SelfLimitDto> Handle(GetSelfLimitByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.SelfLimits
            .Where(o => o.SelfLimitId == request.SelfLimitId)
            .ProjectTo<SelfLimitDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = result ?? throw new EntityNotFoundException(typeof(SelfLimit).Name, request.SelfLimitId);

        return result;
    }
}
