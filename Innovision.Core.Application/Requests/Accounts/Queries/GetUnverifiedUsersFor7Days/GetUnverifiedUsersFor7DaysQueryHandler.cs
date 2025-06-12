using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetUnverifiedUsersFor7Days;

public class GetUnverifiedUsersFor7DaysQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetUnverifiedUsersFor7DaysQuery, UnverifiedAccountVm>
{
  private readonly ICoreDbContext _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<UnverifiedAccountVm> Handle(GetUnverifiedUsersFor7DaysQuery request, CancellationToken cancellationToken)
  {
    var unverifiedAccounts = await _dbContext.Accounts
      .Where(x => x.UserTypeId == Domain.Enums.UserTypes.Player && !x.IsVerified && x.CreatedOn.Date != DateTime.UtcNow.Date)
      .ProjectTo<UnverifiedAccountDto>(_mapper.ConfigurationProvider)
      .ToListAsync(cancellationToken);

    return new UnverifiedAccountVm(unverifiedAccounts);
  }
}
