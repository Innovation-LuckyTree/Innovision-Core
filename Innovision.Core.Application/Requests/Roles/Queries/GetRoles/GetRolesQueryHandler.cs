using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Roles.Queries.GetRoles;

public class GetRolesQueryHandler(IMapper mapper, ICoreDbContext dbContext) : IRequestHandler<GetRolesQuery, ApiResponse<List<UserTypeDto>>>
{
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;

    public async Task<ApiResponse<List<UserTypeDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            List<UserTypeDto> userTypeDtos = new List<UserTypeDto>();

            var query = _dbContext.UserTypes
                .Where(m => !m.IsDeleted
                    && (m.UserTypeName.ToLower() != "newregister")
                    && (m.UserTypeName.ToLower() != "player")
                    && (m.UserTypeName.ToLower() != "master agent")
                    && (m.UserTypeName.ToLower() != "agent"))
                .ProjectTo<UserTypeDto>(_mapper.ConfigurationProvider).AsQueryable();

            if (request.CompanyId.HasValue)
            {
                if (request.CompanyId.Value != -1)
                    query = query.Where(m => m.UserTypeName.ToLower() != "super admin");
            }

            userTypeDtos = await query.ToListAsync(cancellationToken);

            if (request.CompanyId.HasValue)
            {
                if (request.CompanyId.Value != -1)
                {
                    var companyRoles = await _dbContext.UserTypes
                    .Where(m => !m.IsDeleted)
                    .ProjectTo<UserTypeDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                    if (companyRoles != null)
                    {
                        foreach (var item in companyRoles)
                            userTypeDtos.Add(item);
                    }
                }
            }

            return new ApiResponse<List<UserTypeDto>>() { Data = userTypeDtos };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<UserTypeDto>>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
