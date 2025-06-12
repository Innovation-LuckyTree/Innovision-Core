using Innovision.Core.Infrastructure.MessageBrokerClient.Models;

namespace Innovision.Core.Infrastructure.Interfaces;

public interface IMessageBrokerClientApi
{
    Task<CreateRecordResponse> AddRecordAsync<T>(CreateRecordRequest<T> request, CancellationToken cancellationToken) where T: class;
}
