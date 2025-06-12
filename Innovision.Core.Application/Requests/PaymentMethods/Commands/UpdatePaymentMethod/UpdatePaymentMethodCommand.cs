using Innovision.Core.Application.Requests.PaymentMethods.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.PaymentMethods.Commands.UpdatePaymentMethod;

public record UpdatePaymentMethodCommand(int PaymentMethodId, string Name, string Description) : IRequest<PaymentMethodDto>;
