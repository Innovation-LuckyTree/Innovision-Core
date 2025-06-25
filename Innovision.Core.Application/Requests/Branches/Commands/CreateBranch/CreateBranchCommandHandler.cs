using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Exceptions;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Commands.CreateBranch;

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly ICurrentUserService _currentUserService;

    public CreateBranchCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<bool>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        try
        {
            long dfAccountId = 0;
            var branchCount = await _dbContext.Branches.CountAsync(cancellationToken);


            var gfm = await _dbContext.Accounts
                .Where(m => m.IsMain && m.UserTypeId == UserTypes.MasterAgent)
                .FirstOrDefaultAsync(cancellationToken);

            if (gfm == null)
                throw new NameExistsException($"Unable to find GFM.");

            var dfAccount = await _dbContext.Accounts.Where(m => m.RefferralCode == gfm.RefferralKey && m.UserTypeId == UserTypes.Agent).FirstOrDefaultAsync(cancellationToken);
            if (dfAccount != null)
                dfAccountId = dfAccount.AccountInfoId;

            // if still no df recruiter from GFM
            if (dfAccountId == 0)
            {
                // Build default recruiter for GFM
                var dfGfmRec = CreateAccount(request, gfm.BranchId, gfm.RefferralKey, UserTypes.Agent, (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMicroseconds.ToString());
                _dbContext.Accounts.Add(dfGfmRec);
                dfAccountId = dfGfmRec.AccountInfoId;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            var branchNameExist = _dbContext.Branches.Where(e => e.BranchName == request.BranchName).Any();

            if (branchNameExist)
                throw new NameExistsException($"Game Site Name {request.BranchName} already exist");

            var userId = Guid.NewGuid();
            var branch = CreateBranch(request);

            _dbContext.Branches.Add(branch);
            await _dbContext.SaveChangesAsync(cancellationToken);

            if (branchCount == 0)
            {
                branch.GameSiteManagerId = gfm.AccountInfoId;
                branch.GameSiteAccountId = dfAccountId;
                branch.DefaultAccountId = dfAccountId;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                // Build Default GSM
                var dfGsm = CreateAccount(request, branch.BranchId, string.Empty, UserTypes.MasterAgent, "");
                // Buil Default Recruiter for GSM
                var dfrec = CreateAccount(request, branch.BranchId, dfGsm.RefferralKey, UserTypes.Agent, (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMicroseconds.ToString());
                _dbContext.Accounts.Add(dfGsm);
                _dbContext.Accounts.Add(dfrec);

                await _dbContext.SaveChangesAsync(cancellationToken);

                branch.GameSiteManagerId = dfGsm.AccountInfoId;
                branch.GameSiteAccountId = dfrec.AccountInfoId;
                branch.DefaultAccountId = dfAccountId;

                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private Branch CreateBranch(CreateBranchCommand request) =>
        new Branch
        {
            BranchName = request.BranchName,
            Address = new Address
            {
                Region = request.Region,
                Province = request.Province,
                Municipality = request.Municipality,
                Barangay = request.Barangay,
                StreetOrPurok = request.StreetOrPurok
            },
            BranchCode = Guid.NewGuid().ToString().Split('-')[0].ToUpper(),
            IsActive = true,
            IsMain = false,
            CreatedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "System" : _currentUserService.UserId,
            ModifiedBy = string.IsNullOrEmpty(_currentUserService.UserId) ? "System" : _currentUserService.UserId,
            BranchCreditObjectId = Guid.NewGuid()
        };

    private Account CreateAccount(CreateBranchCommand request, int branchId, string refferalCode, int userType, string mobileNum) =>
            new Account
            {
                AccountObjectId = Guid.NewGuid(),
                BranchId = branchId,
                UserId = Guid.NewGuid(),
                RefferralCode = refferalCode,
                RefferralKey = GenerateRefferalCode.GenerateCode(8),
                MobileNumber = (!string.IsNullOrEmpty(mobileNum)) ? mobileNum : request.MobileNumber,
                Commision = 0,

                FirstName = request.BranchName,
                LastName = ((int)UserTypes.MasterAgent == userType) ? "GSM" : "Recruiter",

                IsActive = true,
                AccountStatusId = AccountStatus.Approved,
                UserTypeId = userType, //UserTypes.Agent,
                FmTypeId = ((int)UserTypes.MasterAgent == userType) ? (int)AccountFMTypes.GSM : null,

                CreatedOn = DateTime.UtcNow,
                IsMain = true
            };
}
