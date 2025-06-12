using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Application.Requests.Withdrawals.GetCurrentAccountWithdrawal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetCurrentAccountWithdrawal;

public class GetCurrentAccountWithdrawalQueryHandler(IMediator mediator, ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetCurrentAccountWithdrawalQuery, ApiResponse<WithdrawalInfoVm>>
{
    private readonly IMediator _mediator = mediator;
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<WithdrawalInfoVm>> Handle(GetCurrentAccountWithdrawalQuery request, CancellationToken cancellationToken)
    {
        var response = new ApiResponse<WithdrawalInfoVm>();
        var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        var query = _coreDbContext.Withdrawals
            .Include(o => o.AccountInfo)
                .ThenInclude(o => o.Branch)
            .Where(o => o.AccountInfoId == currentAccount.AccountInfoId)
            .OrderByDescending(o => o.TransactionDate)
            .AsQueryable();

        if (request.Status.HasValue)
            query = query.Where(o => o.Status == request.Status);

        var totalCount = await query.CountAsync(cancellationToken);

        var skipCount = request.PagedQuery.PageSize * request.PagedQuery.PageNumber;

        if (skipCount > 0)
            query = query.Skip(skipCount);

        query = query.Take(request.PagedQuery.PageSize);

        var results = await query.ProjectTo<WithdrawalInfoDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
        response.Data = new WithdrawalInfoVm(results)
        {
            TotalCount = totalCount
        };

        return response;
    }
}