using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.UserRegistration;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.CreateSystemUser;

public class CreateSystemUserCommandHandler(ICoreDbContext dbContext, IAppConfig appConfig, IMediator mediator) : IRequestHandler<CreateSystemUserCommand, ApiResponse<Guid>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IAppConfig _appConfig = appConfig;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<Guid>> Handle(CreateSystemUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            bool hasGfm = false;

            var isExists = _dbContext.Accounts.Where(x => x.MobileNumber == request.UserModel.MobileNumber
                    && (
                            x.AccountStatusId == AccountStatus.ForApproval
                            || x.AccountStatusId == AccountStatus.Migrated
                            || x.AccountStatusId == AccountStatus.Approved
                            || x.AccountStatusId == AccountStatus.Block
                        )
                    ).Any();

            if (isExists)
                return new ApiResponse<Guid>() { Success = false, ErrorMessage = $"Mobile Number:  {request.UserModel.MobileNumber} already exist" };

            var accountInfo = CreateAccount(request.UserModel, request.RoleId, Guid.NewGuid(), Guid.NewGuid(), request.UserModel.BranchId);

            if (request.RoleId == UserTypes.Operator)
            {
                var hasExistingOP = await _dbContext.Accounts.Where(m => m.BranchId == request.UserModel.BranchId).AnyAsync(cancellationToken);
                if (!hasExistingOP)
                    accountInfo.IsMain = true;
            }

            // set 1st master agent as default
            if (request.RoleId == UserTypes.MasterAgent)
            {
                // check if already have GFM
                var branch = await _dbContext.Branches.Where(m => m.BranchId == request.UserModel.BranchId).FirstOrDefaultAsync(cancellationToken);
                var existingGFM = await _dbContext.Accounts.Include(m => m.Branch)
                    .Where(m => m.UserTypeId == UserTypes.MasterAgent && m.IsMain)
                    .FirstOrDefaultAsync(cancellationToken);

                hasGfm = (existingGFM != null) ? true : false;
                if (existingGFM == null)
                {
                    accountInfo.IsMain = true;
                    accountInfo.FmTypeId = 4;
                    accountInfo.Commision = Convert.ToDecimal(_appConfig.GMFCommission);
                }
                else
                {

                    accountInfo.FmTypeId = 1;
                    accountInfo.RefferralCode = existingGFM.RefferralKey;
                }
            }

            // set 1st agent as default
            if (request.RoleId == UserTypes.Agent)
            {
                // get default master agent referal key
                var curBranch = await _dbContext.Branches.Where(m => m.BranchId == request.UserModel.BranchId).FirstOrDefaultAsync();
                if (curBranch != null)
                {
                    var mskey = await _dbContext.Accounts.Where(m => m.AccountInfoId == curBranch.GameSiteManagerId).FirstOrDefaultAsync();
                    if (mskey == null)
                        return new ApiResponse<Guid>() { Success = false, ErrorMessage = "Default Firm Manager is not available in your selected branch" };


                    accountInfo.RefferralCode = mskey.RefferralKey;
                }
            }

            if (request.RoleId == UserTypes.Player && string.IsNullOrEmpty(request.UserModel.ReferralCode))
            {
                var curBranch = await _dbContext.Branches.Where(m => m.BranchId == request.UserModel.BranchId).FirstOrDefaultAsync();
                // get default master agent referal key
                if (curBranch != null)
                {
                    var akey = await _dbContext.Accounts.Where(m => m.AccountInfoId == curBranch.GameSiteAccountId).FirstOrDefaultAsync();

                    if (akey == null)
                        return new ApiResponse<Guid>() { Success = false, ErrorMessage = "Default Agent is not available in your selected branch" };


                    accountInfo.RefferralCode = akey.RefferralKey;
                }
            }

            _dbContext.Accounts.Add(accountInfo);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (request.RoleId == UserTypes.MasterAgent && hasGfm)
            {
                var branch = await _dbContext.Branches.Where(m => m.BranchId == request.UserModel.BranchId).FirstOrDefaultAsync(cancellationToken);
                if (branch != null && branch.GameSiteManagerId == null)
                {
                    branch.GameSiteManagerId = accountInfo.AccountInfoId;
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (request.RoleId == UserTypes.Agent)
            {
                var branch = await _dbContext.Branches.Where(m => m.BranchId == request.UserModel.BranchId).FirstOrDefaultAsync(cancellationToken);
                if (branch != null && branch.GameSiteAccountId == null)
                {
                    branch.GameSiteAccountId = accountInfo.AccountInfoId;
                    await _dbContext.SaveChangesAsync(cancellationToken);
                }
            }
            
            await _mediator.Publish(new AddAccountMigrationNotification(accountInfo.AccountObjectId), cancellationToken).ConfigureAwait(false);

            return new ApiResponse<Guid>() { Data = accountInfo.AccountObjectId };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Guid>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private Account CreateAccount(UserRegistrationCommand request, int roleId, Guid acctObjId, Guid userId, int? branchId) =>
        new()
        {
            AccountObjectId = acctObjId,
            BranchId = branchId.HasValue ? branchId.Value : -1,
            UserId = userId,
            RefferralKey = GenerateRefferalCode.GenerateCode(8),
            RefferralCode = (!string.IsNullOrEmpty(request.ReferralCode)) ? request.ReferralCode : "",
            MobileNumber = request.MobileNumber,
            Commision = request.Commission.HasValue ? request.Commission.Value : 0,
            SalaryRange = request.SalaryRange.HasValue ? request.SalaryRange.Value : null,

            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Nationality = request.Nationality,
            NatureOfWork = request.NatureOfWork,
            SourceOfIncome = request.SourceOfIncome,
            BirthDate = request.BirthDate,
            PlaceOfBirth = request.PlaceOfBirth,

            ValidId = request.ValidId,
            FrontIdPath = request.FrontIdPath,
            SelfiePath = request.SelfiePath,
            BackIdPath = request.BackIdPath,
            SignaturePath = request.SignaturePath,

            Region = request.PresentRegion,
            Province = request.PresentProvince,
            Municipality = request.PresentMunicipality,
            Barangay = request.PresentBarangay,
            StreetOrPurok = request.PresentStreetOrPurok,

            PresentRegion = request.PresentRegion,
            PresentProvince = request.PresentProvince,
            PresentMunicipality = request.PresentMunicipality,
            PresentBarangay = request.PresentBarangay,
            PresentStreetOrPurok = request.PresentStreetOrPurok,

            PermanentRegion = request.PermanentRegion,
            PermanentProvince = request.PermanentProvince,
            PermanentMunicipality = request.PermanentMunicipality,
            PermanentBarangay = request.PermanentBarangay,
            PermanentStreetOrPurok = request.PermanentStreetOrPurok,

            AddressCodes = [new AddressCode
            {
                RegionCode = request.AddressCode.RegionCode,
                ProvinceCode = request.AddressCode.ProvinceCode,
                MunicipalityCode = request.AddressCode.MunicipalityCode,
                BarangayCode = request.AddressCode.BarangayCode,
                PermRegionCode = request.AddressCode.PermRegionCode,
                PermProvinceCode = request.AddressCode.PermProvinceCode,
                PermMunicipalityCode = request.AddressCode.PermMunicipalityCode,
                PermBarangayCode = request.AddressCode.PermBarangayCode,
            }],

            IsActive = true,
            AccountStatusId = AccountStatus.Approved,
            AccountHistories = [new AccountHistory { Action = "APPROVE", CreatedOn = DateTime.UtcNow }],
            UserTypeId = roleId,
            ForVerification = true,

            CreatedOn = DateTime.UtcNow,
            IsMain = false
        };
}
