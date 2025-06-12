namespace Innovision.Core.Infrastructure.MessageBrokerClient.Models;

public record CreateRecordRequest<T>(string TaskType, T TaskData) where T : class;
