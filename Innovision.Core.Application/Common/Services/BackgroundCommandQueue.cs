using Innovision.Core.Application.Common.Interfaces;

namespace Innovision.Core.Application.Common.Services;

public class BackgroundCommandQueue : IBackgroundCommandQueue
{
    private readonly Queue<object> _commands = new();
    private readonly SemaphoreSlim _signal = new(0);

    public void Enqueue<TCommand>(TCommand command)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        lock (_commands)
        {
            _commands.Enqueue(command);
            _signal.Release();
        }

    }

    public async Task<TCommand> DequeueAsync<TCommand>(CancellationToken cancellationToken)
    {
        await _signal.WaitAsync(cancellationToken);
        lock (_commands)
        {
            return (TCommand)_commands.Dequeue();
        }
    }
}
