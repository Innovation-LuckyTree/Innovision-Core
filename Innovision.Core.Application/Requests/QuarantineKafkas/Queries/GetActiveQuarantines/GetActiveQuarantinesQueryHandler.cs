using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.QuarantineKafkas.Queries.GetActiveQuarantines;

public class GetActiveQuarantinesQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetActiveQuarantinesQuery, QuarantineKafkaVm>
{
  private readonly ICoreDbContext _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task<QuarantineKafkaVm> Handle(GetActiveQuarantinesQuery request, CancellationToken cancellationToken)
  {
    var quarantines = await _dbContext.QuarantineKafkas
      .Where(x => x.Status == 1)
      .Take(500) // limit to first 500 records
      .ProjectTo<QuarantineKafkaDto>(_mapper.ConfigurationProvider)
      .ToListAsync(cancellationToken);

    return new QuarantineKafkaVm(quarantines);
  }
}
