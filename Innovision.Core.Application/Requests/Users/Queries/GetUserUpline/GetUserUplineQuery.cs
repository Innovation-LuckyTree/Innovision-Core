using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserUpline
{
    public record GetUserUplineQuery(Guid AccountObjId) : IRequest<ApiResponse<UserUplineDto>>;

    public class GetUserUplineQueryHandler : IRequestHandler<GetUserUplineQuery, ApiResponse<UserUplineDto>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserUplineQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<UserUplineDto>> Handle(GetUserUplineQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var userData = await _dbContext.Accounts.Where(m => m.AccountObjectId == request.AccountObjId).FirstOrDefaultAsync();

                if (userData != null)
                {
                    if (!string.IsNullOrEmpty(userData.RefferralCode))
                    {
                        var uplinneData = await _dbContext.Accounts.Where(m => m.RefferralKey == userData.RefferralCode)
                            .ProjectTo<UserUplineDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(cancellationToken);

                        if (uplinneData != null)
                            return new ApiResponse<UserUplineDto>() { Data = uplinneData };
                    } 
                   
                    if(userData.UserTypeId == UserTypes.Operator)
                    {
                        var superAdminData = await _dbContext.Accounts.Where(m => m.UserTypeId == UserTypes.SuperAdmin)
                            .ProjectTo<UserUplineDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(cancellationToken);

                        if (superAdminData != null)
                            return new ApiResponse<UserUplineDto>() { Data = superAdminData };
                    }
                    else if (userData.UserTypeId == UserTypes.MasterAgent)
                    {
                        var operatorData = await _dbContext.Accounts.Where(m => m.UserTypeId == UserTypes.Operator && m.BranchId == userData.BranchId)
                            .ProjectTo<UserUplineDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(cancellationToken);

                        if (operatorData != null)
                            return new ApiResponse<UserUplineDto>() { Data = operatorData };
                    }
                    else
                    {
                        var masterAgentData = await _dbContext.Accounts.Where(m => m.UserTypeId == UserTypes.MasterAgent && m.BranchId == userData.BranchId)
                            .ProjectTo<UserUplineDto>(_mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync(cancellationToken);

                        if (masterAgentData != null)
                            return new ApiResponse<UserUplineDto>() { Data = masterAgentData };
                    }
                }

                return new ApiResponse<UserUplineDto>() { };
            }
            catch (Exception ex)
            {
                return new ApiResponse<UserUplineDto>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
