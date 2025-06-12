using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateProofInfo;

public class UpdateProofInfoCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<UpdateProofInfoCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<ApiResponse<bool>> Handle(UpdateProofInfoCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not found!" };

            account.ValidId = request.ValidIdType;
            account.FrontIdPath = request.FrontIdPath;
            account.BackIdPath = request.BackIdPath;
            account.SelfiePath = request.SelfiePath;
            account.SignaturePath = request.SignaturePath;
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
