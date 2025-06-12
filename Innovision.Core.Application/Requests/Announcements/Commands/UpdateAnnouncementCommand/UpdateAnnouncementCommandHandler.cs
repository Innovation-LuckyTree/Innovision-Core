using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Innovision.Core.Application.Exceptions;
using AutoMapper;
using Innovision.Core.Application.Requests.Announcements.Queries;

namespace Innovision.Core.Application.Requests.Announcements.Commands.UpdateAnnouncementCommand;

public class UpdateAnnouncementCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<UpdateAnnouncementCommand, AnnouncementDto>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<AnnouncementDto> Handle(UpdateAnnouncementCommand request, CancellationToken cancellationToken)
  {
    var announcement = await _coreDbContext.Announcements
        .Where(o => o.AnnouncementId == request.AnnouncementId)
        .FirstOrDefaultAsync(cancellationToken) ?? throw new EntityNotFoundException("Announcement", request.AnnouncementId);

    announcement.Status = request.Status;
    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return _mapper.Map<AnnouncementDto>(announcement);
  }
}