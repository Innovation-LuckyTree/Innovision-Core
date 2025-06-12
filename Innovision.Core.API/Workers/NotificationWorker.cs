using Innovision.Core.Application.Common.Interfaces;
using MediatR;

namespace Innovision.Core.Workers;

public class NotificationWorker(IBackgroundCommandQueue backgroundCommandQueue, IMediator mediator, ILogger<NotificationWorker> logger) : BackgroundService
{
    private readonly IBackgroundCommandQueue _backgroundCommandQueue = backgroundCommandQueue;
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<NotificationWorker> _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var notification = await _backgroundCommandQueue.DequeueAsync<INotification>(stoppingToken);

                await _mediator.Publish(notification, stoppingToken);
            }
            catch (OperationCanceledException)
            {
                // Graceful shutdown
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error processing command: {ex.Message}");
            }
        }
    }
}
