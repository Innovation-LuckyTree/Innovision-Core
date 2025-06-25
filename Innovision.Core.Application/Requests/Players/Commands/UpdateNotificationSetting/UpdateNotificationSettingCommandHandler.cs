using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateNotificationSetting;

public class UpdateNotificationSettingCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<UpdateNotificationSettingCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<ApiResponse<bool>> Handle(UpdateNotificationSettingCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts
                .Include(m => m.AccountSetting)
                .Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not found!" };

            if (account.AccountSetting != null)
            {
                account.AccountSetting.InAppNotification = request.InAppNotification;
                account.AccountSetting.EmailNotification = request.EmailNotification;
                account.AccountSetting.SmsNotification = request.SmsNotification;
            }
            else
            {
                account.AccountSetting = new Domain.Entity.AccountSetting
                {
                    InAppNotification = request.InAppNotification,
                    EmailNotification = request.EmailNotification,
                    SmsNotification = request.SmsNotification
                };

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
