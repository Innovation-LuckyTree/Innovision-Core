using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Announcements.Queries.GetPaginatedAnnouncementsQuery;

public class GetPaginatedAnnouncementsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetPaginatedAnnouncementsQuery, ApiResponse<AnnouncementVm>>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private const int _defaultBranch = 14;

  public async Task<ApiResponse<AnnouncementVm>> Handle(GetPaginatedAnnouncementsQuery request, CancellationToken cancellationToken)
  {

    var announcementsResponse = new ApiResponse<AnnouncementVm>();

    var announcementsQuery = _coreDbContext.Announcements
        .Include(o => o.Branch)
        .OrderByDescending(o => o.CreatedOn)
        .AsQueryable();

    announcementsQuery = announcementsQuery.Where(o => o.CreatedOn >= request.StartDate && o.CreatedOn <= request.EndDate);

    var start = 0;
    var pageSize = 20;

    var companyId = request.CompanyId;
    var branchId = request.BranchId;
    var SendTo = request.SendTo;

    if (branchId.HasValue)
      announcementsQuery = announcementsQuery.Where(o => o.BranchId == branchId || o.BranchId == _defaultBranch);

    if (SendTo != null)
    {
      var sentToString = string.Join(",", SendTo.Select(id => id.ToString()));
      announcementsQuery = announcementsQuery.Where(o => o.SendTo == sentToString);
    }

    var totalCount = await announcementsQuery.CountAsync(cancellationToken);

    if (!string.IsNullOrEmpty(request.PagedQuery?.Search))
    {
      announcementsQuery = announcementsQuery
        .Where(o => o.Title.Contains(request.PagedQuery.Search));
    }

    if ((request.PagedQuery?.PageSize ?? 0) > 0)
      pageSize = request.PagedQuery!.PageSize;

    if ((request.PagedQuery?.PageNumber ?? 0) > 1)
      start = (request.PagedQuery!.PageNumber - 1) * pageSize;

    var announcements = await announcementsQuery
        .Skip(start)
        .Take(pageSize)
        .ProjectTo<AnnouncementDto>(_mapper.ConfigurationProvider)
        .ToListAsync(cancellationToken);

    announcementsResponse.Data = new AnnouncementVm(announcements)
    {
      Offset = request.PagedQuery?.PageNumber ?? 0,
      TotalCount = totalCount,
      PageSize = pageSize
    };

    return announcementsResponse;
  }
}