using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Roles.Queries.GetRolesByGroupType
{
    public record GetRolesByGroupTypeQuery(GroupTypes groupType, int? CompanyId) : IRequest<ApiResponse<List<UserTypeDto>>>;

    public class GetRolesByGroupTypeQueryHandler : IRequestHandler<GetRolesByGroupTypeQuery, ApiResponse<List<UserTypeDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ICoreDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private const string _serviceProvider = "service provider";
        private string[] _admnUserTypeNames = { "service provider", "super admin" };

        public GetRolesByGroupTypeQueryHandler(IMapper mapper, ICoreDbContext dbContext, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<ApiResponse<List<UserTypeDto>>> Handle(GetRolesByGroupTypeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                List<UserTypeDto> userTypeDtos = new List<UserTypeDto>();

                var adminRoles = await _dbContext.UserTypes
                        .Where(m => _admnUserTypeNames.Contains(m.UserTypeName) && !m.IsDeleted && m.UserTypeId != _currentUserService.RoleId)
                        .ProjectTo<UserTypeDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                if (request.CompanyId.HasValue) {
                    if (request.CompanyId.Value != -1) {
                        var companyRoles = await _dbContext.UserTypes
                        .Where(m => !_admnUserTypeNames.Contains(m.UserTypeName) && !m.IsDeleted
                            //&& m.GroupType == (int)request.groupType 
                            //&& m.UserTypeId != _currentUserService.RoleId
                            && m.UserTypeName.ToLower() != "newregister"
                            && m.UserTypeId != UserTypes.Agent
                            && m.UserTypeId != UserTypes.Player)
                        .ProjectTo<UserTypeDto>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken);

                        return new ApiResponse<List<UserTypeDto>>() { Data = companyRoles };
                    }
                } 

                return new ApiResponse<List<UserTypeDto>>() { Data = adminRoles };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<UserTypeDto>>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
