using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetailsByOrder;

public class GetJackpotDetailsByOrderQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetJackpotDetailsByOrderQuery, JackpotDetailVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<JackpotDetailVm> Handle(GetJackpotDetailsByOrderQuery request, CancellationToken cancellationToken)
    {
        var jackpotDetails = await _coreDbContext.JackpotWinners
            .Include(o => o.JackpotWinnerStatus)
            .Where(o => request.BetTransactionIds.Contains(o.BetTransactionId))
            .ProjectTo<JackpotDetailDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new(jackpotDetails);
    }
}
