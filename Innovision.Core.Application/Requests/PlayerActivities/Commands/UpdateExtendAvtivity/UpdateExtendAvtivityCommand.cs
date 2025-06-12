using Innovision.Core.Application.Common;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.PlayerActivities.Commands.UpdateExtendAvtivity
{
    public class UpdateExtendAvtivityCommand() : IRequest<ApiResponse<long>>
    {
        public long ActivityId { get; set; }
        public int Extended { get; set; } = 0;
    }
    public class UpdateExtendAvtivityCommandHandler(ICoreDbContext coreDbContext) : IRequestHandler<UpdateExtendAvtivityCommand, ApiResponse<long>>
    {
        private readonly ICoreDbContext _coreDbContext = coreDbContext;

        public async Task<ApiResponse<long>> Handle(UpdateExtendAvtivityCommand request, CancellationToken cancellationToken)
        {
            var playerActivity = await _coreDbContext.PlayerActivities.Where(o => o.ActivityId == request.ActivityId).FirstOrDefaultAsync(cancellationToken);
            _ = playerActivity ?? throw new EntityNotFoundException(typeof(PlayerActivity).Name, request.ActivityId);

            playerActivity.Extended = request.Extended;
            playerActivity.LastModified = DateTime.UtcNow;
            playerActivity.RequiredTopay = false;

            _coreDbContext.PlayerActivities.Update(playerActivity);

            await _coreDbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<long>() { Data = playerActivity.ActivityId };
        }
    }
}
