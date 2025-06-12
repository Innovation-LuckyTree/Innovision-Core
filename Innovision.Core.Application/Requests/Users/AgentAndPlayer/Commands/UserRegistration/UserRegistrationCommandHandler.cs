using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Users.AgentAndPlayer.Commands.UserRegistration;

public class UserRegistrationCommandHandler(ICoreDbContext dbContext, IMediator mediator) : IRequestHandler<UserRegistrationCommand, ApiResponse<Guid>>
{
    private const int _defaultBranch = 22;
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<Guid>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isExists = _dbContext.Accounts.Where(x => x.MobileNumber == request.MobileNumber
                    && (
                            x.AccountStatusId == Domain.Enums.AccountStatus.ForApproval
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Migrated
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Approved
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Block
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Completed
                       )
                    ).Any();

            if (isExists)
                return new ApiResponse<Guid>() { Success = false, ErrorMessage = $"Mobile Number:  {request.MobileNumber} already exist" };

            if (string.IsNullOrWhiteSpace(request.ReferralCode))
            {
                var branchData = await _dbContext.Branches.Where(m => m.BranchId == request.BranchId).FirstOrDefaultAsync(cancellationToken);
                var acct = await _dbContext.Accounts.Where(m => m.AccountInfoId == branchData.DefaultAccountId).FirstOrDefaultAsync();
                request.ReferralCode = acct?.RefferralKey;
            }

            var branchId = request?.BranchId ?? _defaultBranch;

            branchId = branchId == 14 ? _defaultBranch : branchId;

            var accountInfo = CreateAccount(request, Guid.NewGuid(), Guid.NewGuid(), branchId);

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

    private Account CreateAccount(UserRegistrationCommand request, Guid acctObjId, Guid userId, int branchId) =>
        new Account
        {
            AccountObjectId = acctObjId,
            BranchId = branchId,
            UserId = userId,
            RefferralCode = (!string.IsNullOrEmpty(request.ReferralCode)) ? request.ReferralCode : string.Empty,
            RefferralKey = GenerateRefferalCode.GenerateCode(8),
            MobileNumber = request.MobileNumber,
            Commision = (request.Commission.HasValue) ? request.Commission.Value : 0,
            SalaryRange = (request.SalaryRange.HasValue) ? request.SalaryRange.Value : null,

            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,
            Nationality = request.Nationality,
            NatureOfWork = request.NatureOfWork,
            SourceOfIncome = request.SourceOfIncome,
            BirthDate = request.BirthDate,
            PlaceOfBirth = request.PlaceOfBirth,
            Gender = request.Sex,
            MartialStatus = request.CivilStatus,

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
            AccountStatusId = Domain.Enums.AccountStatus.Approved,
            UserTypeId = Domain.Enums.UserTypes.Player,
            ForVerification = (!string.IsNullOrEmpty(request.ValidId) && !string.IsNullOrEmpty(request.FrontIdPath) && !string.IsNullOrEmpty(request.BackIdPath)
            && !string.IsNullOrEmpty(request.SignaturePath) && !string.IsNullOrEmpty(request.SelfiePath)) ? true : false,

            CreatedOn = DateTime.UtcNow,
            IsMain = false
        };
}
