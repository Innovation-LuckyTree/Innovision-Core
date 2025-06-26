using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Orders.Commands.CreateBetTransaction;

public class CreateBetTransactionCommandHandler(ICoreDbContext dbContext, IBackgroundCommandQueue backgroundQueue) : IRequestHandler<CreateBetTransactionCommand, long>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IBackgroundCommandQueue _backgroundQueue = backgroundQueue;

    public async Task<long> Handle(CreateBetTransactionCommand request, CancellationToken cancellationToken)
    {
        var betTransaction = new BetTransaction
        {
            AccountInfoId = request.AccountInfoId,
            ReferenceId = request.ReferenceId,
            DrawResultId = request.DrawResultId,
            RoundReference = request.RoundReference,
            GameId = request.GameId,
            BetValue = request.BetValue,
            TransactionType = request.TransactionType,
            AmountBet = request.AmountBet,
            IsBonus = request.IsBonus
        };

        await dbContext.BetTransactions.AddAsync(betTransaction, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        _backgroundQueue.Enqueue(new AddBetTransactionNotification([betTransaction.BetTransactionId]));
        
        return betTransaction.BetTransactionId;
    }
}