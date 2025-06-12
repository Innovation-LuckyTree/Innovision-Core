using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Commands.BulkCreateUser;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateUserDownline
{
    public record UpdateUserDownlineCommand(long AccountInfoId, decimal Commission, int? FmTypeId) : IRequest<ApiResponse<BulkCreateDto>>;
    public class UpdateUserDownlineCommandHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateUserDownlineCommand, ApiResponse<BulkCreateDto>>
    {
        private readonly ICoreDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<BulkCreateDto>> Handle(UpdateUserDownlineCommand request, CancellationToken cancellationToken)
        {

            var user = await _dbContext.Accounts.Where(m => m.AccountInfoId == request.AccountInfoId).FirstOrDefaultAsync(cancellationToken);
            if (user == null)
                return new ApiResponse<BulkCreateDto>() { Success = false, ErrorMessage = "Unable to find user account." };

            var previousCommission = user.Commision;

            user.FmTypeId = request.FmTypeId;
            user.Commision = request.Commission;

            await _dbContext.SaveChangesAsync();

            return new ApiResponse<BulkCreateDto>() { Data = _mapper.Map<BulkCreateDto>(user) };
        }
    }
}
