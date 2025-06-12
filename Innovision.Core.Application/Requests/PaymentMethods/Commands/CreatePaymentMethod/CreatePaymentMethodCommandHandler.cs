using AutoMapper;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.PaymentMethods.Queries;
using MediatR;

namespace Innovision.Core.Application.Requests.PaymentMethods.Commands.CreatePaymentMethod;

public class CreatePaymentMethodCommandHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<CreatePaymentMethodCommand, PaymentMethodDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<PaymentMethodDto> Handle(CreatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = new PaymentMethod
        {
            Name = request.Name,
            Description = request.Description
        };

        _coreDbContext.PaymentMethods.Add(paymentMethod);
        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PaymentMethodDto>(paymentMethod);
    }
}