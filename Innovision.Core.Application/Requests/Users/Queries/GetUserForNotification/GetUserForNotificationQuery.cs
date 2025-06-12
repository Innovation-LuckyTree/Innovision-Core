using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserForNotification
{
    public record GetUserForNotificationQuery(int? CompanyId, int? BranchId) : IRequest<ApiResponse<List<long>>>;
    public class GetUserForNotificationQueryHandler(ICoreDbContext dbContext) : IRequestHandler<GetUserForNotificationQuery, ApiResponse<List<long>>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;

        public async Task<ApiResponse<List<long>>> Handle(GetUserForNotificationQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.CompanyId.HasValue)
                {
                    var companyUsers = await _dbContext.Accounts.Include(m => m.Branch)
                        .Where(m => m.UserTypeId == UserTypes.Operator && m.IsMain)
                        .Select(m => m.AccountInfoId)
                        .ToListAsync();

                    return new ApiResponse<List<long>>() { Data = companyUsers };
                }

                if (request.BranchId.HasValue)
                {
                    var branchUsers = await _dbContext.Accounts
                        .Include(m => m.UserType)
                        .Where(m => m.BranchId == request.BranchId.Value && m.UserTypeId == UserTypes.Operator && m.IsMain
                            || (m.BranchId == request.BranchId && m.UserType.UserTypeName.ToLower().Contains("cashier")))
                        .Select(m => m.AccountInfoId)
                        .ToListAsync();

                    return new ApiResponse<List<long>>() { Data = branchUsers };
                }

                var spUsers = await _dbContext.Accounts.Include(m => m.UserType)
                    .Where(m => m.UserType.UserTypeName.ToLower().Contains("service provider"))
                    .Select(m => m.AccountInfoId).ToListAsync();
                return new ApiResponse<List<long>>() { Data = spUsers };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<long>>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
