using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser;
using Innovision.Core.Common.Models;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetDownlineUsers
{
    public record GetDownlineUsersQuery(long AccountId, string? Search, int? BranchId, DateTime? DateFrom, DateTime? DateTo) : IRequest<ApiResponse<List<BulkCreateDto>>>;
    public class GetDownlineUsersQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetDownlineUsersQuery, ApiResponse<List<BulkCreateDto>>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<List<BulkCreateDto>>> Handle(GetDownlineUsersQuery request, CancellationToken cancellationToken)
        {
            var gfm = await _dbContext.Accounts.Where(m => m.IsMain && m.UserTypeId == UserTypes.MasterAgent).FirstOrDefaultAsync(cancellationToken);

            List<BulkCreateDto> result = new List<BulkCreateDto>();

            if (!string.IsNullOrEmpty(request.Search))
            {
                var query1 = await _dbContext.Accounts
                    .Where(q => (q.FirstName + " " + q.LastName).ToLower().Contains(request.Search.ToLower())
                        && q.AccountInfoId != gfm.AccountInfoId)
                    .OrderByDescending(m => m.AccountInfoId)
                    .ProjectTo<BulkCreateDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                result = query1;
            }
            else
            {
                var uplineUser = await _dbContext.Accounts.Where(m => m.AccountInfoId == request.AccountId).FirstOrDefaultAsync(cancellationToken);
                if (uplineUser == null)
                    return new ApiResponse<List<BulkCreateDto>>() { Success = false, ErrorMessage = "Unable to find upline user account." };

                var query = _dbContext.Accounts
                    .Where(m => m.RefferralCode == uplineUser.RefferralKey && m.AccountInfoId != gfm.AccountInfoId)
                    .OrderByDescending(m => m.AccountInfoId)
                    .AsQueryable();

                if (uplineUser.IsMain && uplineUser.UserTypeId == UserTypes.MasterAgent)
                    query = query.Where(m => m.UserTypeId != UserTypes.Agent);


                var downlineUsers = await query.ProjectTo<BulkCreateDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                result = downlineUsers;
            }

            // get upline user
            var uplines = await _dbContext.Accounts.Where(m => result.Select(m => m.RefferralCode).Contains(m.RefferralKey)).ToListAsync(cancellationToken);
            foreach (var item in result)
            {
                var upline = uplines.Where(m => m.RefferralKey == item.RefferralCode).FirstOrDefault();
                if (upline == null) continue;

                item.UplineCommission = upline.Commision;
            }

            return new ApiResponse<List<BulkCreateDto>>() { Data = result };
        }
    }
}
