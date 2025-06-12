using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Roles.Commands
{
    public record DeleteRoleCommand(int UserTypeId) : IRequest<ApiResponse<int>>;
    public class DeleteRoleCommandHandler(ICoreDbContext dbContext) : IRequestHandler<DeleteRoleCommand, ApiResponse<int>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;

        public async Task<ApiResponse<int>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var query = await _dbContext.UserTypes
                .Where(m => m.UserTypeId == request.UserTypeId).FirstOrDefaultAsync(cancellationToken);

            if (query == null)
                return new ApiResponse<int>() { Success =false };

            query.IsDeleted = true; 
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<int>() { Data = query.UserTypeId };
        }
    }

}
