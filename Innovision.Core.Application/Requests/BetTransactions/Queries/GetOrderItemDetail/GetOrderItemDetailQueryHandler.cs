// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Innovision.Core.Application.Interfaces;
// using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionDetail;

// public class GetBetTransactionDetailQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetBetTransactionDetailQuery, BetTransactionDetailDto>
// {
//     private readonly ICoreDbContext _dbContext = dbContext;
//     private readonly IMapper _mapper = mapper;

//     public async Task<BetTransactionDetailDto> Handle(GetBetTransactionDetailQuery request, CancellationToken cancellationToken)
//     {
//         var BetTransaction = await _dbContext.BetTransactions.Where(e => e.BetTransactionId == request.BetTransactionId && !e.IsDeleted)
//             .ProjectTo<BetTransactionDetailDto>(_mapper.ConfigurationProvider)
//             .FirstOrDefaultAsync(cancellationToken);

//         return BetTransaction;
//     }
// }