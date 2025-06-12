using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Announcements.Queries.GetForNotificationListQuery;

public class GetForNotificationListQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetForNotificationListQuery, AnnouncementVm>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<AnnouncementVm> Handle(GetForNotificationListQuery request, CancellationToken cancellationToken)
  {
    var now = DateTime.UtcNow;

    var query = _coreDbContext.Announcements
        .Where(o => o.Status != 2);

    var totalCount = await query.CountAsync(cancellationToken);

    var result = await query.ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

    var response = new AnnouncementVm(result)
    {
      Offset = 0,
      PageSize = 0,
      TotalCount = totalCount
    };

    return response;
  }
}