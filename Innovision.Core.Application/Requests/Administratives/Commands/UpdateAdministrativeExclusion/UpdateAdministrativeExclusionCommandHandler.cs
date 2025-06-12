using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.UpdateAdministrativeExclusion;

public class UpdateAdministrativeExclusionCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<UpdateAdministrativeExclusionCommand, AdministrativeExclusionDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<AdministrativeExclusionDto> Handle(UpdateAdministrativeExclusionCommand request, CancellationToken cancellationToken)
    {
        var adminExclusion = await _coreDbContext.AdministrativeExclusions
            .Where(o => o.AdministrativeExclusionId == request.AdministrativeExclusionId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = adminExclusion ?? throw new EntityNotFoundException(typeof(AdministrativeExclusion).Name, request.AdministrativeExclusionId);

        adminExclusion.Status = request.Status;
        adminExclusion.DateExpiry = DateTime.UtcNow;

        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AdministrativeExclusionDto>(adminExclusion);
    }
}