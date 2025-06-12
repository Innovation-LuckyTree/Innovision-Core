using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Queries.GetAdminExclusionById;

public class GetAdminExclusionByIdQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetAdminExclusionByIdQuery, AdministrativeExclusionDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<AdministrativeExclusionDto> Handle(GetAdminExclusionByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _coreDbContext.AdministrativeExclusions
            .Where(o => o.AdministrativeExclusionId == request.AdministrativeExclusionId)
            .ProjectTo<AdministrativeExclusionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = result ?? throw new EntityNotFoundException(typeof(AdministrativeExclusion).Name, request.AdministrativeExclusionId);

        return result;
    }
}
