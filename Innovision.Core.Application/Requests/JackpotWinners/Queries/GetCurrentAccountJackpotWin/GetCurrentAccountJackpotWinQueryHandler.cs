using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.JackpotWinners.Queries.GetCurrentAccountJackpotWin;

public class GetCurrentAccountJackpotWinQueryHandler(IMediator mediator, ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetCurrentAccountJackpotWinQuery, ApiResponse<AccountJackpotWinVm>>
{
    private readonly IMediator _mediator = mediator;
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<AccountJackpotWinVm>> Handle(GetCurrentAccountJackpotWinQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<AccountJackpotWinVm>();
        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var query = _coreDbContext.JackpotWinners
            .Include(o => o.Account)
                .ThenInclude(o => o.Branch)
            .Include(o => o.JackpotWinnerStatus)
            .Where(o => o.AccountInfoId == currentAccount.AccountInfoId)
            .OrderByDescending(o => o.JackpotWinnerId)
            .AsQueryable();


        if (request.Status.HasValue)
            query = query.Where(o => o.JackpotWinnerStatusId == request.Status);

        var totalCount = await query.CountAsync(cancellationToken);

        var skipCount = request.PagedQuery.PageSize * request.PagedQuery.PageNumber;

        if (skipCount > 0)
            query = query.Skip(skipCount);

        query = query.Take(request.PagedQuery.PageSize);

        var results = await query.ProjectTo<AccountJackpotWinnerDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        response.Data = new AccountJackpotWinVm(results)
        {
            TotalCount = totalCount
        };

        return response;

    }
}

