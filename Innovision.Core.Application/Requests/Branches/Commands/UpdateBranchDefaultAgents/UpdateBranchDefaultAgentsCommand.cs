using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountNotifications;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Commands.UpdateBranchDefaultAgents;

public class UpdateBranchDefaultAgentsCommand : IRequest<ApiResponse<bool>>
{
    public int BranchId { get; set; }
    public long? MasterAgentInfoId { get; set; }
    public long? AgentInfoId { get; set; }
}

public class UpdateBranchDefaultAgentsCommandHandler(ICoreDbContext dbContext, IMediator mediator, INotificationMessageVm notificationMessage) : IRequestHandler<UpdateBranchDefaultAgentsCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;
    private readonly INotificationMessageVm _notificationMessage = notificationMessage;

    public async Task<ApiResponse<bool>> Handle(UpdateBranchDefaultAgentsCommand request, CancellationToken cancellationToken)
    {
        var notificationMessage = new NotificationMessage();

        try
        {
            var branch = await _dbContext.Branches.Where(o => o.BranchId == request.BranchId).FirstOrDefaultAsync(cancellationToken);

            if (branch == null)
                throw new Exception("Branch not found!");

            if (request.MasterAgentInfoId.HasValue)
            {
                branch.GameSiteManagerId = request.MasterAgentInfoId;

                notificationMessage = _notificationMessage.GetNotificationMessageByName(NotificationNames.AssignDefaultFM);
                await _mediator.Publish(new CreateAccountNotification(request.MasterAgentInfoId.Value, NotificationTypes.GAME_SITE, notificationMessage.Title, notificationMessage.Notifications, notificationMessage.Url), cancellationToken);
            }

            if (request.AgentInfoId.HasValue)
            {
                branch.GameSiteAccountId = request.AgentInfoId;

                notificationMessage = _notificationMessage.GetNotificationMessageByName(NotificationNames.AssignDefaultAgent);
                await _mediator.Publish(new CreateAccountNotification(request.AgentInfoId.Value, NotificationTypes.GAME_SITE, notificationMessage.Title, notificationMessage.Notifications, notificationMessage.Url), cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
