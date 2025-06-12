using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Interfaces;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetLoadUsers
{
    public record GetLoadUsersQuery(int CreditType) : IRequest<ApiResponse<List<SystemUserDto>>>;
    public class GetLoadUsersQueryHandler : IRequestHandler<GetLoadUsersQuery, ApiResponse<List<SystemUserDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILoadingAccountServices _loadingAccountServices;

        public GetLoadUsersQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService, ILoadingAccountServices loadingAccountServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _loadingAccountServices = loadingAccountServices;
        }

        public async Task<ApiResponse<List<SystemUserDto>>> Handle(GetLoadUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<SystemUserDto> respList = new List<SystemUserDto>();
                bool serviceProvider = false;

                var usrObjId = _currentUserService.UserObjId;
                var accountLogin = await _dbContext.Accounts
                    .Include(m => m.Branch)
                    .Include(m => m.UserType)
                        .ThenInclude(m => m.UserTypeAccessControls)
                    .Where(m => m.UserId == usrObjId).FirstOrDefaultAsync(cancellationToken);

                if (accountLogin == null)
                    return new ApiResponse<List<SystemUserDto>>() { Success = false, ErrorMessage = "account not found." };

                if (accountLogin.UserTypeId != (int)UserTypes.Agent && accountLogin.UserTypeId != (int)UserTypes.MasterAgent
                    && accountLogin.UserTypeId != (int)UserTypes.Operator
                    && !accountLogin.UserType.UserTypeName.ToLower().Contains("accounting")
                    && !accountLogin.UserType.UserTypeName.ToLower().Contains("cashier"))
                {
                    serviceProvider = true;
                }

                bool isBranchUser = (accountLogin.UserTypeId != (int)UserTypes.Operator && !accountLogin.IsMain) ? true
                    : (accountLogin.UserType.UserTypeName.ToLower().Contains("cashier")) ? true : false;

                var userControl = accountLogin.UserType.UserTypeAccessControls.FirstOrDefault();
                var results = _loadingAccountServices.GetAccessControl(userControl);

                if (request.CreditType == 0)
                    respList = await _loadingAccountServices.GetUsersList(results.Item2,
                        accountLogin.BranchId, serviceProvider, isBranchUser, accountLogin.UserTypeId, cancellationToken);
                else
                    respList = await _loadingAccountServices.GetUsersList(results.Item1, accountLogin.BranchId, serviceProvider, isBranchUser, accountLogin.UserTypeId, cancellationToken);

                // filter if operator request5
                if (accountLogin.UserTypeId == (int)UserTypes.Operator)
                    respList = FilterForOperator(respList, request.CreditType, accountLogin.IsMain);

                // filter if Accounting request7
                if (accountLogin.UserTypeId == 7)
                    respList = FilterForAccounting(respList, request.CreditType);

                // filter if agent
                if (accountLogin.UserTypeId == (int)UserTypes.Agent)
                    respList = FilterForAgent(respList, request.CreditType, accountLogin.RefferralKey, accountLogin.RefferralCode);

                if (serviceProvider)
                    respList = respList.Where(m => m.IsMain).ToList();

                return new ApiResponse<List<SystemUserDto>>() { Data = respList };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<SystemUserDto>>() { Success = false, ErrorMessage = ex.Message };
            }
        }

        private List<SystemUserDto> FilterForOperator(List<SystemUserDto> systemUserDtos, int creditType, bool isMain)
        {
            if (isMain && creditType == 0)
            {
                var rejectList = systemUserDtos.Where(m => m.UserTypeId == 2 && m.IsMain);
                return systemUserDtos.Except(rejectList).ToList();
            }

            if (isMain && creditType == 1)
            {
                var rejectList = systemUserDtos.Where(m => m.UserTypeId == 2);
                return systemUserDtos.Except(rejectList).ToList();
            }

            if (!isMain && creditType == 1)
                return systemUserDtos.Where(m => m.UserTypeId == 2 && m.IsMain).ToList();

            return systemUserDtos;
        }

        private List<SystemUserDto> FilterForAccounting(List<SystemUserDto> systemUserDtos, int creditType)
        {
            if (creditType == 0)
            {
                var rejectList = systemUserDtos.Where(m => m.UserTypeId == 2 && m.IsMain);
                return systemUserDtos.Except(rejectList).ToList();
            }

            if (creditType == 1)
            {
                var rejectList = systemUserDtos.Where(m => m.UserTypeId == 2);
                return systemUserDtos.Except(rejectList).ToList();
            }

            return systemUserDtos;
        }

        private List<SystemUserDto> FilterForAgent(List<SystemUserDto> systemUserDtos, int creditType, string refferalKey, string refferalCode)
        {
            if (creditType == 0)
            {
                var rejectList = systemUserDtos.Where(m => m.UserTypeId == 4 && m.RefferralCode != refferalKey);
                return systemUserDtos.Except(rejectList).ToList();
            }

            if (creditType == 1)
            {
                var rejectList = systemUserDtos.Where(m => m.RefferralKey != refferalCode);
                return systemUserDtos.Except(rejectList).ToList();
            }

            return systemUserDtos;
        }
    }
}
