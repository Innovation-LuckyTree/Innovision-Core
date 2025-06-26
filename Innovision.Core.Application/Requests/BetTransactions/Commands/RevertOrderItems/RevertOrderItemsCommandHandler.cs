// using Innovision.Core.Application.Interfaces;
// using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
// using Innovision.Core.Application.Requests.Orders.Commands.ScheduleBetTransactions;
// using MediatR;
// using Microsoft.EntityFrameworkCore;

// namespace Innovision.Core.Application.Requests.Orders.Commands.RevertBetTransactions;

// public class RevertBetTransactionsCommandHandler(IMediator mediator, ICoreDbContext dbContext) : IRequestHandler<RevertBetTransactionsCommand, Unit>
// {
//     private readonly IMediator _mediator = mediator;
//     private readonly ICoreDbContext _dbContext = dbContext;

//     public async Task<Unit> Handle(RevertBetTransactionsCommand request, CancellationToken cancellationToken)
//     {
//         var currentAccount = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

//         foreach (var item in request.ScheduleBetTransactions)
//         {
//             var itemOrders = await _dbContext.BetTransactions
//                 .Where(o => item.BetTransactions.Contains(o.BetTransactionId) &&!o.VoidTransaction)
//                 .ToListAsync(cancellationToken);

//             // foreach (var itemOrder in itemOrders)
//             // {
//             //     itemOrder.Used = false;
//             //     itemOrder.UsedDate = null;
//             // }
//         }

//         return Unit.Value;
//     }
// }
