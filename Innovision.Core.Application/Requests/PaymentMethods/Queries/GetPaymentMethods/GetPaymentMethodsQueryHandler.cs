using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PaymentMethods.Queries.GetPaymentMethods;

public class GetPaymentMethodsQueryHandler(ICoreDbContext coreDbContext, IMapper mapper) : IRequestHandler<GetPaymentMethodsQuery, PaymentMethodVm>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<PaymentMethodVm> Handle(GetPaymentMethodsQuery request, CancellationToken cancellationToken)
    {
        var paymentMethods = await _coreDbContext.PaymentMethods
            .ProjectTo<PaymentMethodDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new PaymentMethodVm(paymentMethods);
    }
}

