using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.MasterAgent.Queries.GetCompanyGFM
{
    public record GetCompanyGFMQuery() : IRequest<ApiResponse<BulkCreateDto>>;
    public class GetCompanyGFMQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetCompanyGFMQuery, ApiResponse<BulkCreateDto>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<BulkCreateDto>> Handle(GetCompanyGFMQuery request, CancellationToken cancellationToken)
        {
            var gfm = await _dbContext.Accounts.Include(m => m.Branch)
                .Where(m => m.IsMain && m.UserTypeId == UserTypes.MasterAgent)
                .FirstOrDefaultAsync(cancellationToken);

            if (gfm == null)
                return new ApiResponse<BulkCreateDto>() { Success = false, ErrorMessage = "Unable to find company GFM." };

            return new ApiResponse<BulkCreateDto>() { Data = _mapper.Map<BulkCreateDto>(gfm) };
        }
    }
}
