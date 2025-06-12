namespace Innovision.Core.Application.Common.Interfaces;

public interface IBackgroundCommandQueue
{
    void Enqueue<TCommand>(TCommand command);
    Task<TCommand> DequeueAsync<TCommand>(CancellationToken cancellationToken);    
}
