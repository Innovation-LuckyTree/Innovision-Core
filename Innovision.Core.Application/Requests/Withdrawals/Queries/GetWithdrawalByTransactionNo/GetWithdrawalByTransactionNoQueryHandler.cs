using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Constants;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Withdrawals.Queries.GetWithdrawalByTransactionNo;

public class GetWithdrawalByTransactionNoQueryHandler : IRequestHandler<GetWithdrawalByTransactionNoQuery, ApiResponse<WithdrawalDto>>
{
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;
    public GetWithdrawalByTransactionNoQueryHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
    }
    public async Task<ApiResponse<WithdrawalDto>> Handle(GetWithdrawalByTransactionNoQuery request, CancellationToken cancellationToken)
    {
        var withdrawal = await _dbContext.Withdrawals
            .Where(m => m.TransactionNo.ToLower() == request.transactionNo.ToLower() && m.Status == WalletWithdrawalStatusId.Pending)
            .ProjectTo<WithdrawalDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return new ApiResponse<WithdrawalDto>() { Data = withdrawal };
    }
}