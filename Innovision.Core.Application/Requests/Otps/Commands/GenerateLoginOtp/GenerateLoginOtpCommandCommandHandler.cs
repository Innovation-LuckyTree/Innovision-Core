using Innovision.Core.Application.Common;
using Innovision.Core.Application.Common.Enums;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Otps.Commands.CreateOtp;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateLoginOtp;

public class GenerateLoginOtpCommandCommandHandler : IRequestHandler<GenerateLoginOtpCommand, ApiResponse<LoginOtpDto>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMediator _mediator;
    private readonly int[] _validUserTypes = [Domain.Enums.AccountStatus.Completed, Domain.Enums.AccountStatus.Migrated];

    public GenerateLoginOtpCommandCommandHandler(ICoreDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    public async Task<ApiResponse<LoginOtpDto>> Handle(GenerateLoginOtpCommand request, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts
            //.Where(x => x.MobileNumber == request.MobileNumber && _validUserTypes.Contains(x.AccountStatusId))
            .Where(x => x.MobileNumber == request.MobileNumber)
            .FirstOrDefaultAsync(cancellationToken);

        if (account is null)
            return new ApiResponse<LoginOtpDto>() { Success = false, ErrorMessage = $"Mobile number {request.MobileNumber} not found!" };

        if(!_validUserTypes.Contains(account.AccountStatusId))
            return new ApiResponse<LoginOtpDto>() { Success = false, ErrorMessage = $"Account not migrated! Please contact admin." };

        var otpResponse = new LoginOtpDto();

        try
        {
            var otpReferenceId = await _mediator.Send(new CreateOtpCommand(request.MobileNumber)
            {
                MessageType = (int)SmsMessageTypes.LoginOtp
            }, cancellationToken);

            otpResponse.ReferenceId = otpReferenceId;
            otpResponse.UserId = account.UserId;
            otpResponse.New = account.AccountStatusId == Domain.Enums.AccountStatus.Completed ? false : true;
        }
        catch (Exception ex)
        {
            return new ApiResponse<LoginOtpDto>() { Success = false, ErrorMessage = ex.Message };
        }

        return new ApiResponse<LoginOtpDto> { Data = otpResponse };
    }
}