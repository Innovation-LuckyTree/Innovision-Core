using Innovision.Core.Application.Common;
using Innovision.Core.Application.Requests.Otps.Commands.CreateOtp;
using MediatR;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateOtp;

public class GenerateOtpCommandHandler : IRequestHandler<GenerateOtpCommand, ApiResponse<long>>
{
    private readonly IMediator _mediator;

    public GenerateOtpCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ApiResponse<long>> Handle(GenerateOtpCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var otpReferenceId = await _mediator.Send(new CreateOtpCommand(request.MobileNumber), cancellationToken);

            return new ApiResponse<long>() { Data = otpReferenceId };
        }
        catch (Exception ex)
        {
            return new ApiResponse<long>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}