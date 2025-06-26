// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Innovision.Core.Application.Interfaces;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactions;

// public class GetBetTransactionsQueryHandler : IRequestHandler<GetBetTransactionsQuery, BetTransactionVm>
// {
//     private readonly ICoreDbContext _dbContext;
//     private readonly IMapper _mapper;

//     public GetBetTransactionsQueryHandler(ICoreDbContext dbContext, IMapper mapper)
//     {
//         _dbContext = dbContext;
//         _mapper = mapper;
//     }

//     public async Task<BetTransactionVm> Handle(GetBetTransactionsQuery request, CancellationToken cancellationToken)
//     {
//         var BetTransactions = await _dbContext.BetTransactions.Where(o => request.BetTransactionIds.Contains(o.BetTransactionId) && !o.IsDeleted)
//             .ProjectTo<BetTransactionDto>(_mapper.ConfigurationProvider)
//             .ToListAsync(cancellationToken);

//         return new BetTransactionVm(BetTransactions);
//     }
// }
