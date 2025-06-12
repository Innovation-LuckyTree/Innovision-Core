using AutoMapper;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.PaymentMethods.Queries;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PaymentMethods.Commands.UpdatePaymentMethod;

public class UpdatePaymentMethodCommandHandler : IRequestHandler<UpdatePaymentMethodCommand, PaymentMethodDto>
{
    private readonly ICoreDbContext _coreDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;

    public UpdatePaymentMethodCommandHandler(ICoreDbContext coreDbContext, ICurrentUserService currentUserService, IMapper mapper)
    {
        _coreDbContext = coreDbContext;
        _currentUserService = currentUserService;
        _mapper = mapper;
    }

    public async Task<PaymentMethodDto> Handle(UpdatePaymentMethodCommand request, CancellationToken cancellationToken)
    {
        var paymentMethod = await _coreDbContext.PaymentMethods.FirstOrDefaultAsync(x => x.PaymentMethodId == request.PaymentMethodId, cancellationToken); 

        _ = paymentMethod ?? throw new EntityNotFoundException(typeof(PaymentMethod).Name, request.PaymentMethodId);

        paymentMethod.Name = request.Name;
        paymentMethod.Description = request.Description;
        paymentMethod.LastModified = DateTime.UtcNow;
        paymentMethod.ModifiedBy = _currentUserService.UserId;

        _coreDbContext.PaymentMethods.Update(paymentMethod);
        await _coreDbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<PaymentMethodDto>(paymentMethod);
    }
}