// using Innovision.Core.Application.Interfaces;
// using Innovision.Core.Infrastructure.Interfaces;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Innovision.Core.Application.Requests.Orders.Queries.GetBetTransactionDetail;

// public class GetOrderGrossQueryHandler(ICoreDbContext dbContext, IGamesApi gameApi) : IRequestHandler<GetOrderGrossQuery, OrderGrossVm>
// {
//     private readonly ICoreDbContext _dbContext = dbContext;
//     private readonly IGamesApi _gameApi = gameApi;

//     public async Task<OrderGrossVm> Handle(GetOrderGrossQuery request, CancellationToken cancellationToken)
//     {
//         var orderQuery = _dbContext.Orders
//             .Where(e => e.CreatedOn.Date >= request.DateFrom.Date && e.CreatedOn.Date <= request.DateTo.Date && !e.IsDeleted)
//             .AsQueryable();

//         var orderGrossDaily = await orderQuery.GroupBy(e => new { e.CreatedOn.Date })
//             .Select(g => new OrderGrossDailyDto
//             {
//                 Day = g.Key.Date.Day,
//                 Month = g.Key.Date.Month,
//                 Year = g.Key.Date.Year,
//                 GrossAmount = g.Sum(o => o.TotalAmount),
//                 TotalCount = g.Count(),
//                 Date = g.Key.Date,
//             })
//             .OrderBy(o => o.Date)
//             .ToListAsync(cancellationToken);

//         var advancedBets = await _gameApi.GetAdvancedBets(request.DateFrom, request.DateTo, cancellationToken);

//         var getDeckBets = await orderQuery.SelectMany(o => o.BetTransactions)
//             .Where(o => o.UsedDate == null || o.UsedDate.Value.Date > o.CreatedOn.AddMinutes(3))
//             .GroupBy(o => new { o.CreatedOn.Date })
//             .Select(g => new OrderGrossDailyDto
//             {
//                 Day = g.Key.Date.Day,
//                 Month = g.Key.Date.Month,
//                 Year = g.Key.Date.Year,
//                 GrossAmount = g.Sum(o => o.AmountBet),
//                 TotalCount = g.Count(),
//                 Date = g.Key.Date,
//             })
//             .ToListAsync(cancellationToken);

//         if ((advancedBets?.Count() ?? 0) > 0)
//         {
//             orderGrossDaily = [.. orderGrossDaily.Select(o =>
//             {
//                 var dailyDeckBets = getDeckBets.FirstOrDefault(a => a.Date.Date == o.Date.Date);
//                 if (dailyDeckBets != null)
//                 {
//                     o.DeckAmount = dailyDeckBets.GrossAmount;
//                 }

//                 var advancedBet = advancedBets.FirstOrDefault(a => a.Date.Date == o.Date.Date);

//                 if (advancedBet != null)
//                 {
//                     o.AdvanceAmount = advancedBet.TotalAmount;
//                 }

//                 return o;
//             })];
//         }

//         var orderGrossMontly = orderGrossDaily.GroupBy
//             (e => new { e.Month, e.Year })
//             .Select(g => new OrderGrossMonthlyDto
//             {
//                 Month = g.Key.Month,
//                 Year = g.Key.Year,
//                 DeckAmount = g.Sum(o => o.DeckAmount),
//                 AdvanceAmount = g.Sum(o => o.AdvanceAmount),
//                 GrossAmount = g.Sum(o => o.GrossAmount),
//                 DailyGross = [.. g],
//             }).OrderByDescending(e => e.Year).ThenByDescending(e => e.Month)
//             .ToList();

//         return new OrderGrossVm(orderGrossMontly);
//     }
// }