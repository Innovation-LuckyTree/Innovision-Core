// using AutoMapper;
// using AutoMapper.QueryableExtensions;
// using Innovision.Core.Application.Interfaces;
// using Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsList;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionsByIds;

// public class GetBetTransactionsByIdsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetBetTransactionsByIdsQuery, BetTransactionDetailsVm>
// {
//     private readonly ICoreDbContext _coreDbContext = coreDbContext;
//     private readonly IMapper _mapper = mapper;

//     public async Task<BetTransactionDetailsVm> Handle(GetBetTransactionsByIdsQuery request, CancellationToken cancellationToken)
//     {
//         var BetTransactions = await _coreDbContext.BetTransactions.Where(e => request.BetTransactionIds.Contains(e.BetTransactionId) && !e.IsDeleted)
//             .ProjectTo<BetTransactionDetailDto>(_mapper.ConfigurationProvider)
//             .ToListAsync(cancellationToken);

//         return new BetTransactionDetailsVm(BetTransactions);
//     }
// }
