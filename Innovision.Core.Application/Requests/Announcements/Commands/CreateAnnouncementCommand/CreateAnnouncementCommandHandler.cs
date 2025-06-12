using AutoMapper;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Announcements.Queries;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Announcements.Commands.CreateAnnouncementCommand;

public class CreateAnnouncementCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<CreateAnnouncementCommand, AnnouncementDto>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  public async Task<AnnouncementDto> Handle(CreateAnnouncementCommand request, CancellationToken cancellationToken)
  {
    var sendToString = string.Join(",", request.SendTo.Select(id => id.ToString()));
    Announcement announcement = new()
    {
        BranchId = request.BranchId,
        Title = request.Title,
        Description = request.Description,
        SendTo = string.Join(",", sendToString),
        StartDate = request.StartDate,
        EndDate = request.EndDate,
        IsBanner = request.IsBanner
    };

    _coreDbContext.Announcements.Add(announcement);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return _mapper.Map<AnnouncementDto>(announcement);
  }
}