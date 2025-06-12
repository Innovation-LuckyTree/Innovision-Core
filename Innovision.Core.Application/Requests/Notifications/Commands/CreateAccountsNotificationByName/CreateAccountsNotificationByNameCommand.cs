using Innovision.Core.Application.Requests.Notifications.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Commands.CreateAccountsNotificationByName;

public record CreateAccountsNotificationByNameCommand(IEnumerable<long> Accounts, int NotificationTypeId, string Name, IEnumerable<string>? Parameters = null) : IRequest<IEnumerable<NotificationDto>>;

