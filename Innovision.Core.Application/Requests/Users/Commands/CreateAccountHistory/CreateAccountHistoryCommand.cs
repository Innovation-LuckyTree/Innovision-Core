using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.CreateAccountHistory
{
    public record CreateAccountHistoryCommand(long AccountInfoId, string Action) : IRequest<Unit>;

    public class CreateAccountHistoryCommandHandler(ICoreDbContext dbContext) : IRequestHandler<CreateAccountHistoryCommand, Unit>
    {
        private readonly ICoreDbContext _dbContext = dbContext;

        public async Task<Unit> Handle(CreateAccountHistoryCommand request, CancellationToken cancellationToken)
        {
            var history = new AccountHistory {
                AccountInfoId = request.AccountInfoId,
                Action = request.Action
            };

            _dbContext.AccountHistories.Add(history);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
