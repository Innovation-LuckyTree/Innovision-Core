using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.BasicVerification
{
    public class BasicVerificationCommandHandler(ICoreDbContext dbContext, IMediator mediator) : IRequestHandler<BasicVerificationCommand, ApiResponse<bool>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMediator _mediator = mediator;

        public async Task<ApiResponse<bool>> Handle(BasicVerificationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _dbContext.Accounts //.Include(o => o.Branch)
                    .Where(x => x.AccountObjectId == request.AccountObjectId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (user == null)
                    return new ApiResponse<bool>() { Success = false, ErrorMessage = $"Account not found!" };

                user.MobileNumber = request.MobileNumber;
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.MiddleName = request.MiddleName;
                user.Suffix = request.Suffix;
                user.NatureOfWork = request.NatureOfWork;
                user.SourceOfIncome = request.SourceOfIncome;
                user.BirthDate = request.BirthDate;
                user.SalaryRange = request.SalaryRange;

                user.FrontIdPath = request.FrontIdPath;
                user.SelfiePath = request.SelfiePath;
                user.BackIdPath = request.BackIdPath;

                user.ForVerification = false;
                user.IsVerified = true;
                user.IsActive = true;

                await _dbContext.SaveChangesAsync();
                return new ApiResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
