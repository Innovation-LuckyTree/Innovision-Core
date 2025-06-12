using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Users.Queries.ValidateMobileNumber;

public class ValidateMobileNumberQueryHandler : IRequestHandler<ValidateMobileNumberQuery, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext;

    public ValidateMobileNumberQueryHandler(ICoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<bool>> Handle(ValidateMobileNumberQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var isExists = _dbContext.Accounts.Where(x => x.MobileNumber == request.MobileNumber
                    && (
                            x.AccountStatusId == Domain.Enums.AccountStatus.ForApproval
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Migrated
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Approved
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Block
                            || x.AccountStatusId == Domain.Enums.AccountStatus.Completed
                       )
                    ).Any();

            if (isExists)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = $"Mobile Number:  {request.MobileNumber} already exist" };

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
