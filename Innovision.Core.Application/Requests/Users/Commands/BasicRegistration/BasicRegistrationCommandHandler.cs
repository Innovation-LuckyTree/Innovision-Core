using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Domain.Entity;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Commands.BasicRegistration;

public class BasicRegistrationCommandHandler(ICoreDbContext dbContext, IMediator mediator) : IRequestHandler<BasicRegistrationCommand, ApiResponse<Guid>>
{
    private int _defaultBranch = 1;
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;


    public async Task<ApiResponse<Guid>> Handle(BasicRegistrationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isExists = _dbContext.Accounts.Where(x => x.MobileNumber == request.MobileNumber
                    && RegisteredAccountStatus.EXISTING_ACCOUNT_STATUS.Contains(x.AccountStatusId)).Any();

            if (isExists)
                return new ApiResponse<Guid>() { Success = false, ErrorMessage = $"Mobile Number:  {request.MobileNumber} already exist" };

            var accountInfo = CreateAccount(request, Guid.NewGuid(), Guid.NewGuid(), _defaultBranch);

            _dbContext.Accounts.Add(accountInfo);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new AddAccountMigrationNotification(accountInfo.AccountObjectId), cancellationToken).ConfigureAwait(false);

            return new ApiResponse<Guid>() { Data = accountInfo.AccountObjectId };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Guid>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private Account CreateAccount(BasicRegistrationCommand request, Guid acctObjId, Guid userId, int branchId) =>
        new()
        {
            AccountObjectId = acctObjId,
            BranchId = branchId,
            UserId = userId,
            RefferralCode = (!string.IsNullOrEmpty(request.ReferralCode)) ? request.ReferralCode : string.Empty,
            RefferralKey = GenerateRefferalCode.GenerateCode(8),
            MobileNumber = request.MobileNumber,
            UserName = request.UserName,
            Commision = 0,
            SalaryRange = null,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true,
            AccountStatusId = Domain.Enums.AccountStatus.Approved,
            UserTypeId = Domain.Enums.UserTypes.Player,
            ForVerification = false,
            CreatedOn = DateTime.UtcNow,
            IsMain = false,
            ScreenName = request.UserName
        };
}
