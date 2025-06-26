using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Exceptions;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Users.Operator.Commands;

public class CreateOperatorCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService
         , IAccountServices accountServices, IMediator mediator) : IRequestHandler<CreateOperatorCommand, ApiResponse<Unit>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IAccountServices _accountServices = accountServices;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<Unit>> Handle(CreateOperatorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var branchNameExist = await _dbContext.Branches.Where(e => e.BranchId == request.BranchId).AnyAsync(cancellationToken);

            if (!branchNameExist)
                throw new NameExistsException($"Branch ID {request.BranchId} is not exist");

            var userId = Guid.NewGuid();

            //Operator
            var branchOperator = _accountServices.GenerateCreateUserModel(request.Details, userId, true, false, _currentUserService);
            branchOperator.BranchId = request.BranchId;

            _dbContext.Accounts.Add(branchOperator);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AddAccountMigrationNotification(branchOperator.AccountObjectId), cancellationToken).ConfigureAwait(false);

            return new ApiResponse<Unit>() { Data = Unit.Value };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Unit>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}

