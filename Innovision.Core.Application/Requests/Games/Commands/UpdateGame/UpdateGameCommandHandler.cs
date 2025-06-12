using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Games.Commands.UpdateGame;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
{
    private readonly ICoreDbContext _dbContext;

    public UpdateGameCommandHandler(ICoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
    {
        var game = await _dbContext.Games.Where(o => o.GameId == request.GameId)
            .FirstOrDefaultAsync(cancellationToken);
        
        _ = game ?? throw new EntityNotFoundException(typeof(Game).Name, request.GameId);

        game.Name = request.Name;
        game.Description = request.Description;

        _dbContext.Games.Update(game);
        await _dbContext.SaveChangesAsync(cancellationToken);
    
        return Unit.Value; 
    }
}