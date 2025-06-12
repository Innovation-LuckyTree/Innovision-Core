using Innovision.Core.Application.Common;
using MediatR;

namespace Innovision.Core.Application.Requests.Notifications.Queries.GetCredtiNotifcationById;
public record GetCredtiNotifcationByIdQuery(long CreditTransId) : IRequest<ApiResponse<NotificationRecipient>>;
