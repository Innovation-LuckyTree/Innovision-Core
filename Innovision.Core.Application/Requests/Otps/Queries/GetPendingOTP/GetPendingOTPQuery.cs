using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Request.Otps.Queries.GetPendingOTP
{
    public class GetPendingOTPQuery : IRequest<ApiResponse<List<GetPendingOTPQueryDto>>>
    {
    }

    public class GetPendingOTPQueryHandler : IRequestHandler<GetPendingOTPQuery, ApiResponse<List<GetPendingOTPQueryDto>>>
    {
        private readonly ICoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetPendingOTPQueryHandler(ICoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<GetPendingOTPQueryDto>>> Handle(GetPendingOTPQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var otps = await _dbContext.Otps
                   .Where(o => !o.IsVerify)
                   .ProjectTo<GetPendingOTPQueryDto>(_mapper.ConfigurationProvider)
                   .ToListAsync(cancellationToken);

                return new ApiResponse<List<GetPendingOTPQueryDto>>() { Data = otps };
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<GetPendingOTPQueryDto>>() { Success = false, ErrorMessage = ex.Message };
            }
        }
    }
}
