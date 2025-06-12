using Innovision.Core.Application.Requests.PaymentMethods.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.PaymentMethods.Commands.CreatePaymentMethod;

public record CreatePaymentMethodCommand(string Name, string Description) : IRequest<PaymentMethodDto>;
