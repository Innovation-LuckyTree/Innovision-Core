using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Branches.Commands.UpdateBranch
{
    public class UpdateBranchCommand : IRequest<ApiResponse<bool>>
    {
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public string Region { get; set; }
        public string Province { get; set; }
        public string Municipality { get; set; }
        public string Barangay { get; set; }
        public string StreetOrPurok { get; set; }
    }

    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, ApiResponse<bool>>
    {
        private readonly ICoreDbContext _dbContext;

        public UpdateBranchCommandHandler(ICoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse<bool>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var branch = await _dbContext.Branches.Where(o => o.BranchId == request.BranchId).FirstOrDefaultAsync(cancellationToken);

                if (branch == null)
                    throw new Exception("Branch not found!");

                branch.BranchName = request.BranchName;
                branch.Address.Region = request.Region;
                branch.Address.Province = request.Province;
                branch.Address.Municipality = request.Municipality;
                branch.Address.Barangay = request.Barangay;
                branch.Address.StreetOrPurok = request.StreetOrPurok;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new ApiResponse<bool>() { Data = true };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
