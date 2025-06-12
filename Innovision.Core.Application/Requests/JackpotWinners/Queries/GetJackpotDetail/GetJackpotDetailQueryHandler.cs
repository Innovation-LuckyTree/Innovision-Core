
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetJackpotDetail;

public class GetJackpotDetailQueryHandler(ICoreDbContext coreDbContext, IMapper mapper, IMediator mediator) : IRequestHandler<GetJackpotDetailQuery, JackpotWinnerDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<JackpotWinnerDto> Handle(GetJackpotDetailQuery request, CancellationToken cancellationToken)
    {
        var account = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var jackpotWinner = await _coreDbContext.JackpotWinners
            .Include(o => o.Account)
                .ThenInclude(e => e.Branch)
            .Include(o => o.JackpotWinnerAttachments)
            .Include(o => o.JackpotWinnerStatus)
            .Where(o => o.JackpotWinnerId == request.JackpotWinnerId)
            .ProjectTo<JackpotWinnerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = jackpotWinner ?? throw new EntityNotFoundException(typeof(JackpotWinner).Name, request.JackpotWinnerId);

        if (account != null && account.AccountInfoId != jackpotWinner.AccountInfoId)
        {
            throw new EntityNotFoundException(typeof(JackpotWinner).Name, request.JackpotWinnerId);
        }

        return jackpotWinner;
    }
}