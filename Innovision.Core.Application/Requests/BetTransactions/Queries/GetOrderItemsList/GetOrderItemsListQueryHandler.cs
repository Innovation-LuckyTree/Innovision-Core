// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Innovision.Core.Application.Interfaces;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;

// public class GetBetTransactionsListQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetBetTransactionsListQuery, BetTransactionDetailVm>
// {
//     private readonly ICoreDbContext _dbContext = dbContext;
//     private readonly IMapper _mapper = mapper;

//     public async Task<BetTransactionDetailVm> Handle(GetBetTransactionsListQuery request, CancellationToken cancellationToken)
//     {
//         var BetTransactionList = await _dbContext.BetTransactions.Where(e => e.BetTransactionId > request.BetTransactionId && !e.IsDeleted)
//             .Include(e => e.AccountInfo)
//                 .ThenInclude(s => s.Branch)
//                     .ThenInclude(b => b.Company)
//             .Include(e => e.GameType)
//                 .ThenInclude(g => g.Game)
//             .OrderBy(e => e.BetTransactionId)
//             .ProjectTo<BetTransactionDetailDto>(_mapper.ConfigurationProvider)
//             .Take(request.Size)
//             .ToListAsync(cancellationToken);
        
//         var lastItemId = BetTransactionList.Select(o => o.BetTransactionId).OrderByDescending(o => o).FirstOrDefault();

//         return new BetTransactionDetailVm(BetTransactionList, lastItemId);
//     }
// }