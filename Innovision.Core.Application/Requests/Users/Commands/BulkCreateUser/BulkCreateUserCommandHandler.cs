using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser;

public class BulkCreateUserCommandHandler(ICoreDbContext dbContext, IMapper mapper, IMediator mediator) : IRequestHandler<BulkCreateUserCommand, ApiResponse<List<BulkCreateDto>>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<List<BulkCreateDto>>> Handle(BulkCreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var accountInfos = request.Users.Select(obj => CreateAccount(obj, request.BranchId, request.ReferralCode, request.UserTypeId));

            _dbContext.Accounts.AddRange(accountInfos);
            await _dbContext.SaveChangesAsync();

            // Set default agent/firm manager
            var branch = await _dbContext.Branches.Where(m => m.BranchId == request.BranchId).FirstOrDefaultAsync();
            if (accountInfos.Count() > 0 && request.UserTypeId == UserTypes.MasterAgent)
            {
                if (branch != null && branch.GameSiteManagerId == null)
                {
                    branch.GameSiteManagerId = accountInfos.First().AccountInfoId;
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (accountInfos.Count() > 0 && request.UserTypeId == UserTypes.Agent)
            {
                if (branch != null && branch.GameSiteAccountId == null)
                {
                    branch.GameSiteAccountId = accountInfos.First().AccountInfoId;
                    await _dbContext.SaveChangesAsync();
                }
            }

            foreach(var account in accountInfos)
            {
                await _mediator.Publish(new AddAccountMigrationNotification(account.AccountObjectId), cancellationToken).ConfigureAwait(false);
            }

            return new ApiResponse<List<BulkCreateDto>>() { Data = _mapper.Map<List<BulkCreateDto>>(accountInfos) };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<BulkCreateDto>>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private Account CreateAccount(BulkUser request, int branchId, string refferalCode, int usertypeId) =>
        new Account
        {
            AccountObjectId = Guid.NewGuid(),
            BranchId = branchId,
            UserId = Guid.NewGuid(),
            RefferralCode = refferalCode,
            RefferralKey = GenerateRefferalCode.GenerateCode(8),
            MobileNumber = request.MobileNumber,
            Commision = request.Commission,

            FirstName = request.FirstName,
            LastName = request.LastName,
            MiddleName = request.MiddleName,

            IsActive = true,
            AccountStatusId = AccountStatus.Approved,
            AccountHistories = [new AccountHistory { Action = "APPROVE", CreatedOn = DateTime.UtcNow }],
            UserTypeId = usertypeId,
            FmTypeId = (request.Position > 0) ? request.Position : null,

            CreatedOn = DateTime.UtcNow,
            IsMain = false,
            ForVerification = false
        };
}
