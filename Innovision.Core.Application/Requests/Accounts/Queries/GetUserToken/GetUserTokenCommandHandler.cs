using Innovision.Core.Application.Common;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetUserToken;

public class GetUserTokenCommandHandler(ICoreDbContext dbContext, ICoreIdentityApi coreIdentityApi) : IRequestHandler<GetUserTokenCommand, ApiResponse<LoginResponse>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICoreIdentityApi _coreIdentityApi = coreIdentityApi;

    public async Task<ApiResponse<LoginResponse>> Handle(GetUserTokenCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accInfo = await _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.Branch)
                .Where(o => (o.MobileNumber == request.UserName || o.Email == request.UserName || request.UserName == o.UserName)
                    && o.UserTypeId != Domain.Enums.UserTypes.Player && o.IsActive)
                .ToListAsync(cancellationToken);

            if ((accInfo?.Count ?? 0) == 0) throw new EntityNotFoundException("AccountInfo", request.UserName);

            // Connect to core identity API to get token credentials
            var result = await _coreIdentityApi.LoginUser(request.UserName, request.Password, request.IpAddress, cancellationToken);

            _ = result ?? throw new Exception("User not found! Please contact admin.");                

            var migratedUser = accInfo?.Where(x => x.UserId == result.Id).FirstOrDefault();
            _ = migratedUser ?? throw new Exception("Account not migrated! Please contact admin.");

            var gameSiteAccountCode = await GetGameSiteAccountCode(migratedUser.BranchId, cancellationToken);

            var userData = new LoginResponse
            {
                Id = result.Id,
                AccountObjectId = migratedUser.AccountObjectId,
                IdNumber = result.IdNumber,
                UserName = result.UserName,
                Token = result.Token,
                RefreshToken = result.RefreshToken,
                ClientId = result.ClientId,
                ExpirationDate = result.ExpirationDate,
                Status = result.Status,
                BranchId = migratedUser.BranchId,
                BranchName = migratedUser.Branch.BranchName,
                Fullname = $"{migratedUser.FirstName} {migratedUser.LastName}",
                AccountInfoId = migratedUser.AccountInfoId,
                AccountCreditId = migratedUser.AccountCreditId,
                IsMain = migratedUser.IsMain,
                UserTypeId = migratedUser.UserType.UserTypeId,
                UserTypeName = migratedUser.UserType.UserTypeName,
                GroupType = migratedUser.UserType.GroupType,
                RoleType = migratedUser.UserType.RoleType,
                BranchCreditObjectId = migratedUser.Branch.BranchCreditObjectId.Value,
                AccountBonusId = migratedUser.AccountBonusId,
                FmTypeId = migratedUser.FmTypeId,
                ReferralCode = gameSiteAccountCode,
                BranchBonusObjectId = migratedUser.Branch.BranchBonusObjectId.Value,
            };

            return new ApiResponse<LoginResponse>() { Data = userData };
        }
        catch (Exception ex)
        {
            return new ApiResponse<LoginResponse>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private async Task<string> GetGameSiteAccountCode(int branchId, CancellationToken cancellationToken)
    {
        var siteAccount = await _dbContext.Branches.Where(b => b.BranchId == branchId).FirstOrDefaultAsync();

        if (siteAccount == null)
            return "";

        var gameSiteAccount = await _dbContext.Accounts.Where(o => o.AccountInfoId == siteAccount.GameSiteAccountId).FirstOrDefaultAsync(cancellationToken);

        if ((gameSiteAccount?.RefferralKey ?? "") != "")
            return gameSiteAccount.RefferralKey;

        var defaultAccount = await _dbContext.Accounts.Where(o => o.AccountInfoId == siteAccount.DefaultAccountId).FirstOrDefaultAsync(cancellationToken);

        return defaultAccount?.RefferralKey ?? "";
    }
}