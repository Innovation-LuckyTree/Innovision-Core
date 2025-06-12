using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.VerifyOtp;

public class VerifyOtpCommandHandler : IRequestHandler<VerifyOtpCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext;

    public VerifyOtpCommandHandler(ICoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<bool>> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        // NOTE : The database time and sever time must be the same.
        //var otp = _dbContext.OTP.Where(x => x.Code == request.OtpCode
        //     && x.MobileNumber == request.MobileNumber
        //     //&& x.ExpireDate >= DateTime.UtcNow 
        //     //&& x.ExpireDate <= DateTime.UtcNow).FirstOrDefault();
        try
        {
            var otp = _dbContext.Otps.Where(x => x.Code == request.OtpCode
                && x.MobileNumber == request.MobileNumber).FirstOrDefault();

            if (otp == null)
                return new ApiResponse<bool>() { Success = false, Data = false };

            otp.IsVerify = true;

            _dbContext.Otps.Update(otp);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new ApiResponse<bool>() { Data = true };
        }
        catch (Exception ex)
        {
            return new ApiResponse<bool>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}