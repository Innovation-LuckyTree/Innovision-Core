using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Games.Commands.CreateGame;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, Unit>
{
    private readonly ICoreDbContext _dbContext;

    public CreateGameCommandHandler(ICoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Unit> Handle(CreateGameCommand request, CancellationToken cancellationToken)
    {
        var gameRequest = new Game
        {
            Name = request.Name,
            Description = request.Description
        };

        _dbContext.Games.Add(gameRequest);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}