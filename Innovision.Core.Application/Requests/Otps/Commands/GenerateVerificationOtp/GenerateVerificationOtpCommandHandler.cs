using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Otps.Commands.CreateOtp;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateVerificationOtp
{
    public class GenerateVerificationOtpCommandHandler : IRequestHandler<GenerateVerificationOtpCommand, ApiResponse<long>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMediator _mediator;

        public GenerateVerificationOtpCommandHandler(ICoreDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ApiResponse<long>> Handle(GenerateVerificationOtpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _dbContext.Accounts
                    .Where(x => x.MobileNumber == request.MobileNumber
                    && x.UserType.UserTypeId == UserTypes.Player).FirstOrDefaultAsync(cancellationToken);

                if (account is null)
                    return new ApiResponse<long>() { Success = false, ErrorMessage = $"Mobile number {request.MobileNumber} does not exist!" };


                var referenceId = await _mediator.Send(new CreateOtpCommand(request.MobileNumber), cancellationToken);
                return new ApiResponse<long> { Data = referenceId };
            }
            catch (Exception ex)
            {
                return new ApiResponse<long>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
