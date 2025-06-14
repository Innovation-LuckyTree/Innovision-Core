using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Application.Requests.Accounts.Commands.AddToUserIdentityWIthCredential;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Renci.SshNet.Messages;
using System.Globalization;

namespace Innovision.Core.Application.Requests.Users.Commands.BasicRegistration
{
    public class BasicRegistrationCommand : IRequest<ApiResponse<Guid>>
    {
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string? ReferralCode { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }

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

                //if (string.IsNullOrWhiteSpace(request.ReferralCode))
                //{
                //    var acct = await _dbContext.Accounts.Where(m => m.RefferralKey == request.ReferralCode).FirstOrDefaultAsync();
                //    request.ReferralCode = acct?.RefferralKey;

                //    _defaultBranch = acct.BranchId;
                //}

                var accountInfo = CreateAccount(request, Guid.NewGuid(), Guid.NewGuid(), _defaultBranch);

                _dbContext.Accounts.Add(accountInfo);
                await _dbContext.SaveChangesAsync(cancellationToken);

                //await _mediator.Publish(new AddAccountMigrationNotification(accountInfo.AccountObjectId), cancellationToken).ConfigureAwait(false);
                await _mediator.Send(new AddToIdentityWIthCredCommand(accountInfo.AccountObjectId, request.Password), cancellationToken);

                return new ApiResponse<Guid>() { Data = accountInfo.AccountObjectId };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>() { Success = false, ErrorMessage = ex.Message };
            }
        }

        private Account CreateAccount(BasicRegistrationCommand request, Guid acctObjId, Guid userId, int branchId) =>
            new Account
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

                FirstName = (request.FullName.Split().Length > 0) ? request.FullName.Split()[0] : string.Empty,
                LastName = (request.FullName.Split().Length > 1) ? request.FullName.Split()[1] : string.Empty,

                IsActive = true,
                AccountStatusId = Domain.Enums.AccountStatus.Approved,
                UserTypeId = Domain.Enums.UserTypes.Player,
                ForVerification = false,

                CreatedOn = DateTime.UtcNow,
                IsMain = false
            };
    }
}
