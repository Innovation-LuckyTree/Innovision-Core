using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Notifications.AccountHistoryNotification;
using Innovision.Core.Application.Notifications.MessageBrokers;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateUserStatuses;

public class UpdateUserStatusesCommandHandler(ICoreDbContext dbContext, IMediator mediator) : IRequestHandler<UpdateUserStatusesCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<bool>> Handle(UpdateUserStatusesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.IsActive.HasValue)
                await SetAccountToActive(request, cancellationToken);

            if (request.Status.HasValue)
                await UpdateUserStatus(request, cancellationToken);

            if (request.SubStatus.HasValue)
                await UpdateUserSubStatus(request, cancellationToken);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }

    private async Task SetAccountToActive(UpdateUserStatusesCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts.Where(m => m.AccountInfoId == request.AccountInfoId).FirstOrDefaultAsync(cancellationToken);
        if (account != null)
        {
            account.IsActive = request.IsActive.Value;
            account.LastModified = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            var msg = request.IsActive.Value ? "Active" : "InActive";

            await _mediator.Publish(new AccountUpdateMigrationNotification(account.AccountObjectId), cancellationToken).ConfigureAwait(false);
            await _mediator.Publish(new AccountHistoryNotification(request.AccountInfoId, $"{msg}"), cancellationToken);
        }
    }

    private async Task UpdateUserStatus(UpdateUserStatusesCommand request, CancellationToken cancellationToken)
    {
        var statusData = await _dbContext.UserStatuses
            .FirstOrDefaultAsync(m => m.AccountInfoId == request.AccountInfoId);

        if (statusData == null)
        {
            statusData = new UserStatus
            {
                AccountInfoId = request.AccountInfoId
            };
            _dbContext.UserStatuses.Add(statusData);
        }

        statusData.Status = request.Status.Value;
        await _dbContext.SaveChangesAsync(cancellationToken);

        // for verification
        if (request.Status.Value < 2)
        {
            var account = await _dbContext.Accounts.Where(m => m.AccountInfoId == request.AccountInfoId).FirstOrDefaultAsync(cancellationToken);
            if (account != null)
            {
                account.IsVerified = (request.Status.Value == 1) ? true : false;
                account.LastModified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new AccountUpdateMigrationNotification(account.AccountObjectId), cancellationToken).ConfigureAwait(false);
            }
        }

        string msg = request.Status.Value switch
        {
            0 => "Semi-Verified",
            1 => "Fully Verified",
            2 => "Removed",
            3 => "Closed",
            4 => "Terminated",
            5 => "Banned",
            _ => "Others",
        };
        await _mediator.Publish(new AccountHistoryNotification(request.AccountInfoId, $"{msg}"), cancellationToken);
    }

    private async Task UpdateUserSubStatus(UpdateUserStatusesCommand request, CancellationToken cancellationToken)
    {
        var statusData = await _dbContext.UserStatuses.FirstOrDefaultAsync(m => m.AccountInfoId == request.AccountInfoId);
        if (statusData == null)
        {
            statusData = new UserStatus
            {
                AccountInfoId = request.AccountInfoId
            };

            _dbContext.UserStatuses.Add(statusData);
        }

        statusData.SubStatus = request.SubStatus.Value;
        await _dbContext.SaveChangesAsync(cancellationToken);

        string msg = request.SubStatus.Value switch
        {
            0 => "Compliant",
            1 => "Non Compliant",
            2 => "Warning",
            3 => "3 Days Suspension",
            4 => "7 Days Suspension",
            5 => "30 Days Suspension",
            6 => "Terminated",
            7 => "Banned",
            8 => "Locked",
            9 => "Self Exclusion",
            10 => "Administrative Exclusion",
            11 => "Dormant",
            _ => "Others",
        };

        await _mediator.Publish(new AccountHistoryNotification(request.AccountInfoId, $"{msg}"), cancellationToken);
    }
}
