using AutoMapper;
using Innovision.Core.Application.Common.Enums;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentAccountInfo;
using Innovision.Core.Application.Requests.Deposits.Queries;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Deposits.Commands.AddUserDepositRequest;

public class SaveUserDepositTransactionCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, ICurrentUserService currentUser, IMediator mediator) : IRequestHandler<SaveUserDepositTransactionCommand, DepositDto>
{
    private readonly ICoreDbContext _coreDbContext = coreDbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ICurrentUserService _currentUser = currentUser;
    private readonly IMediator _mediator = mediator;

    public async Task<DepositDto> Handle(SaveUserDepositTransactionCommand request, CancellationToken cancellationToken)
    {
        var currrentUser = await _mediator.Send(new GetCurrentAccountInfoQuery(), cancellationToken);

        Deposit deposit = new()
        {
            AccountInfoId = currrentUser.AccountInfoId,
            Amount = request.Amount,
            DepositStatusId = (int)DepositStatusTypes.Success,
            PaymentMethodId = (int)PaymentMethodTypes.GCash,
            TransactionDate = DateTime.UtcNow,
            Remarks = request.Remarks,
            TransactionType = request.TransactionType
        };

        _coreDbContext.Deposits.Add(deposit);

        try
        {
            await _coreDbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            
        }

        return _mapper.Map<DepositDto>(deposit);
    }
}