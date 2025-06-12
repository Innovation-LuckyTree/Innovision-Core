using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Queries.GetUserByInfoId
{
    public record GetUserByInfoIdCommand(long AccountInfoId) : IRequest<ApiResponse<SystemUser>>;

    public class GetUserByInfoIdCommandHandler : IRequestHandler<GetUserByInfoIdCommand, ApiResponse<SystemUser>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByInfoIdCommandHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse<SystemUser>> Handle(GetUserByInfoIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _dbContext.Accounts
                    .Include(m => m.UserType)
                    .Include(m => m.UserStatuses)
                    .Include(m => m.Branch)
                    .Where(m => m.AccountInfoId == request.AccountInfoId).AsQueryable();

                var userInfo = await query
                    .ProjectTo<SystemUser>(_mapper.ConfigurationProvider)
                    .OrderByDescending(x => x.CreatedOn)
                    .FirstOrDefaultAsync(cancellationToken);

                return new ApiResponse<SystemUser>() { Data = userInfo };
            }
            catch (Exception ex)
            {
                return new ApiResponse<SystemUser>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }

}
