using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.CoreIdentity.Models.Requests;
using Innovision.Core.Infrastructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Commands.ResetUserPassword
{
    public class ResetUserPasswordCommand : IRequest<Unit>
    {
        public Guid AccountObjectId { get; set; }
        public string MobileNumber { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class ResetUserPasswordCommandHandler(ICoreDbContext dbContext, ICoreIdentityApi coreIdentityApi) : IRequestHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly ICoreIdentityApi _coreIdentityApi = coreIdentityApi;

        public async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var account = await _dbContext.Accounts.Where(o => o.AccountObjectId == request.AccountObjectId && o.MobileNumber == request.MobileNumber)
                .FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                throw new EntityNotFoundException("Account", request.AccountObjectId);

            var passwordRequest = new UpdateUserPasswordRequest(account.UserId, request.NewPassword, request.ConfirmPassword);

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
}
