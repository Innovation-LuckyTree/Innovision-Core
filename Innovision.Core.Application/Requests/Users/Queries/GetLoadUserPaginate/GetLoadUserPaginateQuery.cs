using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetLoadUserPaginate
{
    public record GetLoadUserPaginateQuery(int CreditType, PagedQuery? PagedQuery) : IRequest<ApiResponse<SystemUserVm>>;
    public class GetLoadUserPaginateQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService, ILoadingAccountServices loadingAccountServices) 
        : IRequestHandler<GetLoadUserPaginateQuery, ApiResponse<SystemUserVm>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;
        private readonly ICurrentUserService _currentUserService = currentUserService;
        private readonly ILoadingAccountServices _loadingAccountServices = loadingAccountServices;

        public async Task<ApiResponse<SystemUserVm>> Handle(GetLoadUserPaginateQuery request, CancellationToken cancellationToken)
        {
            try
            {
                SystemUserVm respVm = new SystemUserVm();
                bool serviceProvider = false;

                var usrObjId = _currentUserService.UserObjId;
                var accountLogin = await _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Include(m => m.UserType)
                        .ThenInclude(m => m.UserTypeAccessControls)
                    .Where(m => m.UserId == usrObjId).FirstOrDefaultAsync(cancellationToken);

                if (accountLogin == null)
                    return new ApiResponse<SystemUserVm>() { Success = false, ErrorMessage = "account not found." };

                if (accountLogin.UserType.UserTypeName.ToLower().Contains("service provider"))
                    serviceProvider = true;

                UserTypeConfig? userControl = new UserTypeConfig();

                if (accountLogin.IsMain)
                    userControl = accountLogin.UserType.UserTypeAccessControls.Where(m => m.IsMainUser).FirstOrDefault();

                // If still null then get the default
                if (userControl.Id == 0)
                    userControl = accountLogin.UserType.UserTypeAccessControls.FirstOrDefault();

                var results = _loadingAccountServices.GetAccessControl(userControl);

                var CashInLevel = (userControl != null) ? userControl.CashInLevel : null;
                var RequestLevel = (userControl != null) ? userControl.RequestLevel : null;

                // Cash In
                if (request.CreditType == 0)
                    respVm = await _loadingAccountServices.GetUsersListPaginate(results.Item2,
                        accountLogin.Branch.BranchId, serviceProvider, request.PagedQuery, usrObjId, CashInLevel, cancellationToken);
                // Request
                else
                    respVm = await _loadingAccountServices.GetUsersListPaginate(results.Item1,
                        accountLogin.Branch.BranchId, serviceProvider, request.PagedQuery, usrObjId, RequestLevel, cancellationToken);

                return new ApiResponse<SystemUserVm> { Data = respVm };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SystemUserVm>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
