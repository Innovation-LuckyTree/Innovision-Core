using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdateAddress;

public class UpdateAddressCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<UpdateAddressCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<ApiResponse<bool>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts
                // .Include(m => m.AddressCodes)
                .Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not found!" };

            if (!string.IsNullOrEmpty(request.PresentRegion) && !string.IsNullOrEmpty(request.PresentProvince)
                && !string.IsNullOrEmpty(request.PresentMunicipality) && !string.IsNullOrEmpty(request.PresentBarangay)
                && !string.IsNullOrEmpty(request.PresentStreetOrPurok))
            {
                account.Region = request.PresentRegion;
                account.Province = request.PresentProvince;
                account.Municipality = request.PresentMunicipality;
                account.Barangay = request.PresentBarangay;
                account.StreetOrPurok = request.PresentStreetOrPurok;

                account.PresentRegion = request.PresentRegion;
                account.PresentProvince = request.PresentProvince;
                account.PresentMunicipality = request.PresentMunicipality;
                account.PresentBarangay = request.PresentBarangay;
                account.PresentStreetOrPurok = request.PresentStreetOrPurok;

                //account.AddressCodes.FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(request.PermanentRegion) && !string.IsNullOrEmpty(request.PermanentProvince)
                && !string.IsNullOrEmpty(request.PermanentMunicipality) && !string.IsNullOrEmpty(request.PermanentBarangay)
                && !string.IsNullOrEmpty(request.PermanentStreetOrPurok))
            {
                account.PermanentRegion = request.PermanentRegion;
                account.PermanentProvince = request.PermanentProvince;
                account.PermanentMunicipality = request.PermanentMunicipality;
                account.PermanentBarangay = request.PermanentBarangay;
                account.PermanentStreetOrPurok = request.PermanentStreetOrPurok;
            }

            account.LastModified = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}

