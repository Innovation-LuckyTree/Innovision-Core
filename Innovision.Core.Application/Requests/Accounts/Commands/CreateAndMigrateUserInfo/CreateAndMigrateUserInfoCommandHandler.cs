using Innovision.Core.Application.Common.Exceptions;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Commands.CreateAndMigrateUserInfo;

public class CreateAndMigrateUserInfoCommandHandler : IRequestHandler<CreateAndMigrateUserInfoCommand, Unit>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IAppConfig _appConfig;
    private readonly ICoreIdentityApi _coreIdentityApi;
    private readonly ICurrentUserService _currentAccountService;

    public CreateAndMigrateUserInfoCommandHandler(ICoreDbContext dbContext, IAppConfig appConfig, ICurrentUserService currentAccountService, ICoreIdentityApi coreIdentityApi)
    {
        _dbContext = dbContext;
        _appConfig = appConfig;
        _currentAccountService = currentAccountService;
        _coreIdentityApi = coreIdentityApi;
    }

    public async Task<Unit> Handle(CreateAndMigrateUserInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isExists = _dbContext.Accounts.Where(x => x.MobileNumber == request.MobileNumber).Any();

            if (isExists)
                throw new NameExistsException($"Mobile Number:  {request.MobileNumber} already exist");

            var refferalCodeExists = _dbContext.Accounts
                .Include(o => o.Branch)
                .Where(x => x.RefferralKey == request.ReferralCode).FirstOrDefault();

            _ = refferalCodeExists ?? throw new NameExistsException($"Reference Code {request.ReferralCode} is not exist");

            var accountInfo = CreateAccount(request, Guid.NewGuid(), Guid.NewGuid(), refferalCodeExists.BranchId);

            _dbContext.Accounts.Add(accountInfo);
            await _dbContext.SaveChangesAsync(cancellationToken);

            // Now migrate user

            var appId = Guid.Parse(_appConfig.AppId);
            if (accountInfo.UserTypeId == Domain.Enums.UserTypes.Player)
                appId = Guid.Parse(_appConfig.MobileAppId);

            var createUserRequest = new CreateUserRequest(accountInfo.MobileNumber ?? accountInfo.Email, accountInfo.Email ?? "", accountInfo.MobileNumber,
                string.Empty, accountInfo.UserTypeId, accountInfo.IsMain, appId, "");

            var response = await _coreIdentityApi.CreateUserIdentity(createUserRequest, cancellationToken);

            // Then update user after migration.

            accountInfo.AccountStatusId = Domain.Enums.AccountStatus.Migrated;
            accountInfo.UserId = response.Id;
            accountInfo.LastModified = DateTime.UtcNow;
            accountInfo.ModifiedBy = _currentAccountService.UserId;
            accountInfo.LastSetPassword = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            throw new CoreIdentityApiException("Create and Migrate User Account", ex.ToString());
        }
    }

    private Account CreateAccount(CreateAndMigrateUserInfoCommand request, Guid acctObjId, Guid userId, int branchId) =>
        new()
        {
            AccountObjectId = acctObjId,
            BranchId = branchId,
            UserId = userId,
            RefferralCode = request.ReferralCode,
            FirstName = request.FirstName,
            LastName = request.LastName,
            MobileNumber = request.MobileNumber,
            IsActive = true,
            AccountStatusId = Domain.Enums.AccountStatus.Approved,
            UserTypeId = Domain.Enums.UserTypes.Player,

            CreatedOn = DateTime.UtcNow,
            CreatedBy = acctObjId.ToString(),
            IsMain = true
        };
}
