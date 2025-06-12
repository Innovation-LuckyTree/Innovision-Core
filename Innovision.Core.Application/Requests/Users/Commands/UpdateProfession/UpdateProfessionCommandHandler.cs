using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateProfession;

public class UpdateProfessionCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<UpdateProfessionCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<ApiResponse<bool>> Handle(UpdateProfessionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not found!" };

            account.NatureOfWork = request.NatureOfWork;
            account.SourceOfIncome = request.SourceOfIncome;
            account.SalaryRange = request.SalaryRange;

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
