using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Announcements.Queries.GetPendingAnnouncementsQuery;

public class GetPendingAnnouncementsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetPendingAnnouncementsQuery, AnnouncementVm>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private const int _defaultBranch = 14;

  public async Task<AnnouncementVm> Handle(GetPendingAnnouncementsQuery request, CancellationToken cancellationToken)
  {
    var now = DateTime.UtcNow.Date;

    var query = _coreDbContext.Announcements
        .Where(o => o.IsBanner &&
                    o.Status != 1 &&
                    (o.BranchId == -1 || o.BranchId == request.BranchId || o.BranchId == _defaultBranch) &&
                    o.StartDate.Value.Date <= now && o.EndDate.Value.Date >= now)
        .OrderByDescending(o => o.CreatedOn)
        .AsQueryable();


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