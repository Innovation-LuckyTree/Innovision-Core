using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Commands.UpdateGameMissedDraw
{
    public record UpdateGameMissedDrawCommand(int GameId, int StandardMissedDraws) : IRequest<Unit>;
    public class UpdateGameMissedDrawCommandHandler(ICoreDbContext dbContext) : IRequestHandler<UpdateGameMissedDrawCommand, Unit>
    {
        private readonly ICoreDbContext _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateGameMissedDrawCommand request, CancellationToken cancellationToken)
        {
            var game = await _dbContext.Games.Where(o => o.GameId == request.GameId)
                .FirstOrDefaultAsync(cancellationToken);

            _ = game ?? throw new EntityNotFoundException(typeof(Game).Name, request.GameId);

            _dbContext.Games.Update(game);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
