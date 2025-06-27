using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Commands.UpdateUserByAccountObjectId;

public class UpdateUserByAccountObjectIdCommandHandler(ICoreDbContext dbContext, IMediator mediator) : IRequestHandler<UpdateUserByAccountObjectIdCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMediator _mediator = mediator;

    public async Task<ApiResponse<bool>> Handle(UpdateUserByAccountObjectIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _dbContext.Accounts.Include(o => o.Branch)
                .Where(x => x.AccountObjectId == request.AccountObjectId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = $"Mobile Number:  {request.MobileNumber} already exist" };


            user.BranchId = request.BranchId;
            user.MobileNumber = request.MobileNumber;

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.MiddleName = request.MiddleName;
            user.Nationality = request.Nationality;
            user.NatureOfWork = request.NatureOfWork;
            user.SourceOfIncome = request.SourceOfIncome;
            user.BirthDate = request.BirthDate;
            user.PlaceOfBirth = request.PlaceOfBirth;
            user.Gender = request.Sex ??"";
            user.MartialStatus = request.CivilStatus ?? "";
            user.SalaryRange = request.SalaryRange;

            user.ValidId = request.ValidId;
            user.FrontIdPath = request.FrontIdPath;
            user.SelfiePath = request.SelfiePath;
            user.BackIdPath = request.BackIdPath;
            user.SignaturePath = request.SignaturePath;

            user.PresentAddress.Region = request.PresentRegion;
            user.PresentAddress.Province = request.PresentProvince;
            user.PresentAddress.Municipality = request.PresentMunicipality;
            user.PresentAddress.Barangay = request.PresentBarangay;
            user.PresentAddress.StreetOrPurok = request.PresentStreetOrPurok;

            user.PermanentAddress.Region = request.PermanentRegion;
            user.PermanentAddress.Province = request.PermanentProvince;
            user.PermanentAddress.Municipality = request.PermanentMunicipality;
            user.PermanentAddress.Barangay = request.PermanentBarangay;
            user.PermanentAddress.StreetOrPurok = request.PermanentStreetOrPurok;

            user.ForVerification = true;
            user.IsActive = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            //await _mediator.Send(new SendNotificationCommand(user.Branch.CompanyId), cancellationToken).ConfigureAwait(false);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
