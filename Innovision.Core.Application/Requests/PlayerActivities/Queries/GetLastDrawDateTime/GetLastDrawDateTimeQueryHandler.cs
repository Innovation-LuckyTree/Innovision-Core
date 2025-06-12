using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Queries.GetLastDrawDateTime;

public class GetLastDrawDateTimeQueryHandler : IRequestHandler<GetLastDrawDateTimeQuery, DateTime?>
{
  private readonly ICoreDbContext _coreDbContext;

  public GetLastDrawDateTimeQueryHandler(ICoreDbContext coreDbContext)
  {
    _coreDbContext = coreDbContext;
  }

  public async Task<DateTime?> Handle(GetLastDrawDateTimeQuery request, CancellationToken cancellationToken)
  {
    var latestDrawDateTime = await _coreDbContext.PlayerActivities
        .OrderByDescending(o => o.LastDrawDateTime)
        .FirstOrDefaultAsync(cancellationToken);

    return latestDrawDateTime?.LastDrawDateTime.Value.DateTime;
  }
}
