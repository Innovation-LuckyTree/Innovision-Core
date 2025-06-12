using Innovision.Core.Domain.Entity;
using Innovision.Core.Application.Interfaces;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.CreateOtp;

public class CreateOtpCommandHandler : IRequestHandler<CreateOtpCommand, long>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMediator _mediator;

    public CreateOtpCommandHandler(ICoreDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<long> Handle(CreateOtpCommand request, CancellationToken cancellationToken)
    {
        Random rnd = new Random();
        var randomNumber = (rnd.Next(100000, 999999)).ToString();

        var otp = new OTP
        {
            MobileNumber = request.MobileNumber,
            Code = randomNumber,
            IsVerify = false,
            CreatedOn = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMinutes(30)
        };

        _dbContext.Otps.Add(otp);

        await _dbContext.SaveChangesAsync();
        
        var otpMessage = $"DO NOT SHARE YOUR OTP. Your ONE-TIME PIN(OTP) is {randomNumber}. Enter within 5 minutes to proceed";
        
        //await _mediator.Publish(new SmsQueueingNotification(request.MobileNumber, otpMessage)
        //{
        //    MessageType = request.MessageType
        //}, cancellationToken);

        return otp.OtpID;
    }
}