using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Commands.UpdatePersonalDetails;

public class UpdatePersonalDetailsCommandHandler(ICoreDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<UpdatePersonalDetailsCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly ICurrentUserService _currentUserService = currentUserService;

    public async Task<ApiResponse<bool>> Handle(UpdatePersonalDetailsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts.Where(x => x.UserId == _currentUserService.UserObjId).FirstOrDefaultAsync(cancellationToken);

            if (account == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage = "Account not found!" };

            account.FirstName = request.FirstName;
            account.MiddleName = request.MiddleName;
            account.LastName = request.LastName;
            account.BirthDate = request.BirthDate;
            account.Nationality = request.Nationality;
            account.Gender = request.Gender;
            account.MartialStatus = request.MartialStatus;
            account.PlaceOfBirth = request.PlaceOfBirth;

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
