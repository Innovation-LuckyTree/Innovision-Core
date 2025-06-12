using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.Otps.Commands.CreateOtp;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Otps.Commands.GenerateRegistrationOtp
{
    public class GenerateRegistrationOtpCommandHandler : IRequestHandler<GenerateRegistrationOtpCommand, ApiResponse<long>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMediator _mediator;

        public GenerateRegistrationOtpCommandHandler(ICoreDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task<ApiResponse<long>> Handle(GenerateRegistrationOtpCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var account = await _dbContext.Accounts
                    .Where(x => x.MobileNumber == request.MobileNumber).FirstOrDefaultAsync(cancellationToken);

                if (account is not null)
                    return new ApiResponse<long>() { Success = false, ErrorMessage = $"Mobile number {request.MobileNumber} already exist!" };

            
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
