using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Users.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Otps.Commands.VerifyVerificationOTP;

public class VerifyVerificationOTPCommandHandler : IRequestHandler<VerifyVerificationOTPCommand, ApiResponse<UnverifiedUsers>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public VerifyVerificationOTPCommandHandler(ICoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UnverifiedUsers>> Handle(VerifyVerificationOTPCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var otp = _dbContext.Otps.Where(x => x.OtpID == request.ReferenceId && x.Code == request.OtpCode
                && x.MobileNumber == request.MobileNumber).FirstOrDefault();

            if (otp == null)
                return new ApiResponse<UnverifiedUsers>() { Success = false, ErrorMessage = "Code Invalid. Submit another code or Resend to get a new one." };

            otp.IsVerify = true;

            _dbContext.Otps.Update(otp);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var query = _dbContext.Accounts
                .Include(m => m.UserType)
                .Include(m => m.Branch)
                .Where(x => x.MobileNumber == request.MobileNumber).AsQueryable();

            var userInfo = await query
                .ProjectTo<UnverifiedUsers>(_mapper.ConfigurationProvider)
                .OrderByDescending(x => x.CreatedOn)
                .FirstOrDefaultAsync(cancellationToken);

            return new ApiResponse<UnverifiedUsers>() { Data = userInfo };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UnverifiedUsers>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}