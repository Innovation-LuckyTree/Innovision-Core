using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.VerifyOtp;

public class VerifyV2OtpCommandHandler : IRequestHandler<VerifyV2OtpCommand, ApiResponse<bool>>
{
    private readonly ICoreDbContext _dbContext;

    public VerifyV2OtpCommandHandler(ICoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<bool>> Handle(VerifyV2OtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var otp = _dbContext.Otps.Where(x => x.OtpID == request.ReferenceId && x.Code == request.OtpCode
                && x.MobileNumber == request.MobileNumber).FirstOrDefault();

            if (otp == null)
                return new ApiResponse<bool>() { Success = false, ErrorMessage="Code Invalid. Submit another code or Resend to get a new one." };

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