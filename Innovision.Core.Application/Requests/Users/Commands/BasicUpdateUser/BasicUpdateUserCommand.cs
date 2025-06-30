using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.BasicUpdateUser
{
    public class BasicUpdateUserCommand : IRequest<ApiResponse<bool>>
    {
        public Guid UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? MartialStatus { get; set; }
        public string? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? MobileNumber { get; set; }
    }

    public class BasicUpdateUserCommandHandler(ICoreDbContext dbContext) : IRequestHandler<BasicUpdateUserCommand, ApiResponse<bool>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        public async Task<ApiResponse<bool>> Handle(BasicUpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _dbContext.Accounts.Where(x => x.UserId == request.UserId).FirstOrDefaultAsync(cancellationToken);

                if (account == null)
                    return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not found!" };

                account.FirstName = request?.FirstName ?? account.FirstName;
                account.LastName = request?.LastName ?? account.LastName;
                account.BirthDate = request?.BirthDate ?? account.BirthDate;
                account.Gender = request?.Gender ?? account.Gender;
                account.MartialStatus = request?.MartialStatus ?? account.MartialStatus;
                account.Email = request?.Email ?? account.Email;
                account.MobileNumber = request?.MobileNumber ?? account.MobileNumber;

                account.LastModified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ApiResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
