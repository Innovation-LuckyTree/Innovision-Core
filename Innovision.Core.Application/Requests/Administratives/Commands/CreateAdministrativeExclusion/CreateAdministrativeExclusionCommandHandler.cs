using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using MediatR;

namespace Innovision.Core.Application.Requests.ApplicationVersions.Commands.CreateAdministrativeExclusion;

public class CreateAdministrativeExclusionCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IWebsocketServicesApi websocketServiceApi) : IRequestHandler<CreateAdministrativeExclusionCommand, AdministrativeExclusionDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IWebsocketServicesApi _websocketServiceApi = websocketServiceApi;


    public async Task<AdministrativeExclusionDto> Handle(CreateAdministrativeExclusionCommand request, CancellationToken cancellationToken)
    {
        if ((request?.AccountId ?? 0) == 0)
            throw new EntityNotFoundException(typeof(Account).Name, request.AccountId);

        var dateExpiry = DateTime.UtcNow.AddDays(request.DayDuration).AddHours(request.TimeDuration.Hours);

        AdministrativeExclusion adminExclusion = new()
        {
            AccountId = request.AccountId,
            DayDuration = request.DayDuration,
            TimeDuration = request.TimeDuration,
            DateExpiry = dateExpiry,
            Status = 1
        };

        _coreDbContext.AdministrativeExclusions.Add(adminExclusion);
        await _coreDbContext.SaveChangesAsync(cancellationToken);

        await NotifyAccount(request.AccountId, cancellationToken);

        return _mapper.Map<AdministrativeExclusionDto>(adminExclusion);
    }

    private async Task NotifyAccount(long accountId, CancellationToken cancellationToken)
    {
      await Task.Run(async () => await _websocketServiceApi.AdminExclusion(new CreateAdminExclusionRequest(accountId), cancellationToken), cancellationToken);
    }
}