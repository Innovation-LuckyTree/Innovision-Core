using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Exceptions;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Common.Services;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Innovision.Core.Application.Requests.Accounts.Users.MasterAgent.Commands.CreateMasterAgent
{
    public class CreateMasterAgentCommand : IRequest<ApiResponse<Unit>>
    {
        public int BranchId { get; set; }
        public Details Details { get; set; }
    }

    public class CreateMasterAgentCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService
             , IAccountServices accountServices, IMediator mediator) : IRequestHandler<CreateMasterAgentCommand, ApiResponse<Unit>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly IAccountServices _accountServices = accountServices;
        private readonly IMediator _mediator = mediator;

        public async Task<ApiResponse<Unit>> Handle(CreateMasterAgentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var branchNameExist = _dbContext.Branches.Where(e => e.BranchId == request.BranchId).Any();

                if (!branchNameExist)
                    throw new NameExistsException($"Branch ID {request.BranchId} is not exist");

                var userId = Guid.NewGuid();

                var userObj = _accountServices.GenerateCreateUserModel(request.Details, userId, true, false, _currentUserService);
                userObj.BranchId = request.BranchId;

                userObj.UserTypeId = UserTypes.MasterAgent;
                userObj.RefferralKey = GenerateRefferalCode.GenerateCode(8);
                userObj.RefferralCode = GenerateRefferalCode.GenerateCode(8);
                userObj.AccountStatusId = AccountStatus.Approved;

                _dbContext.Accounts.Add(userObj);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new AddAccountMigrationNotification(userObj.AccountObjectId), cancellationToken).ConfigureAwait(false);

                return new ApiResponse<Unit>() { Data = Unit.Value };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Unit>() { Success=false, ErrorMessage=ex.Message };
            }
        }
    }
}
