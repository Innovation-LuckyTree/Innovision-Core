using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotWinnersByGame;

public class GetJackpotWinnersByGameScheduleQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetJackpotWinnersByGameScheduleQuery, JackpotWinnerInfoVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<JackpotWinnerInfoVm> Handle(GetJackpotWinnersByGameScheduleQuery request, CancellationToken cancellationToken)
    {
        var results = await _coreDbContext.JackpotWinners
            .Where(o => o.GameScheduleId == request.GameScheduleId)
            .ProjectTo<JackpotWinnerInfo>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new JackpotWinnerInfoVm(results);
    }
}