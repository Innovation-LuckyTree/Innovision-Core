using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Commands.AddGameType;

public class AddGameTypeCommandHandler : IRequestHandler<AddGameTypeCommand, Unit>
{
    private readonly ICoreDbContext _dbContext;

    public AddGameTypeCommandHandler(ICoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(AddGameTypeCommand request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games.Where(o => o.GameId == request.GameId)
            .FirstOrDefaultAsync(cancellationToken);

        _ = game ?? throw new EntityNotFoundException(typeof(Game).Name, request.GameId);

        var gameTypeRequest = new GameType
        {
            GameId = request.GameId,
            GameReferenceId = request.GameReferenceId,
            GameTypeName = request.GameTypeName,
            GameTypeDesciption = request.GameTypeDesciption
        };

        _dbContext.GameTypes.Add(gameTypeRequest);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}