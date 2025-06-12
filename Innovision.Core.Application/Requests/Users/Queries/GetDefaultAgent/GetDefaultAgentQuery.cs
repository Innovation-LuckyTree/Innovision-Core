using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetDefaultAgent
{
    public record GetDefaultAgentQuery(int? BranchId) : IRequest<ApiResponse<List<BulkCreateDto>>>;
    public class GetDefaultAgentQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetDefaultAgentQuery, ApiResponse<List<BulkCreateDto>>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<List<BulkCreateDto>>> Handle(GetDefaultAgentQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Accounts
                .Include(m => m.Branch)
                .Where(m => m.IsMain && m.UserTypeId == UserTypes.Agent)
                .OrderByDescending(m => m.AccountInfoId)
                .AsQueryable();

            if (request.BranchId.HasValue)
                query = query.Where(m => m.BranchId == request.BranchId);

            var defaultUsers = await query.ProjectTo<BulkCreateDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ApiResponse<List<BulkCreateDto>>() { Data = defaultUsers };
        }
    }
}
