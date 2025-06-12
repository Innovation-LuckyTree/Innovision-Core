using FluentValidation;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Commands.UpdateUserPassword;

public class UpdateUserPasswordCommandHandler : IRequestHandler<UpdateUserPasswordCommand, Unit>
{
    private readonly ICoreDbContext _dbContext;
    private readonly ICoreIdentityApi _coreIdentityApi;

    public UpdateUserPasswordCommandHandler(ICoreDbContext dbContext, ICoreIdentityApi coreIdentityApi)
    {
        _dbContext = dbContext;
        _coreIdentityApi = coreIdentityApi;
    }

    public async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var isOtpValidated = await _dbContext.Otps
            .Where(o => o.OtpID == request.OtpReferenceId && o.MobileNumber == request.MobileNumber && o.IsVerify)
            .AnyAsync(cancellationToken);

        if (!isOtpValidated)
            throw new EntityNotFoundException("OTP", request.OtpReferenceId);

        var account = await _dbContext.Accounts.Where(o => o.UserId == request.UserId && o.MobileNumber == request.MobileNumber)
            .FirstOrDefaultAsync(cancellationToken);

        if (account == null)
            throw new EntityNotFoundException("Account", request.UserId);

        var passwordRequest = new UpdateUserPasswordRequest(request.UserId, request.NewPassword, request.ConfirmPassword);

        await _coreIdentityApi.UpdateUserPassword(passwordRequest, cancellationToken);
        
        account.LastSetPassword = DateTime.UtcNow;
        // if successful password update then complete the user account status
        if (account.AccountStatusId != AccountStatus.Completed)
        {
            account.AccountStatusId = AccountStatus.Completed;
        }

        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync();

        return Unit.Value;
    }
}