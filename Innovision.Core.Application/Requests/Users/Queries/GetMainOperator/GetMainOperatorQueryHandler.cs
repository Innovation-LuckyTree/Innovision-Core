using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.Operator.Queries.GetMainOperator;

public class GetMainOperatorQueryHandler(ICoreDbContext dbContext, IMapper mapper) : IRequestHandler<GetMainOperatorQuery, ApiResponse<MainOperatorDto>>
{
    private readonly ICoreDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiResponse<MainOperatorDto>> Handle(GetMainOperatorQuery request, CancellationToken cancellationToken)
    {
        var opUser = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.IsMain && m.UserTypeId == UserTypes.Operator)
            .FirstOrDefaultAsync(cancellationToken);

        if (opUser == null)
            return new ApiResponse<MainOperatorDto>() { Success = false, ErrorMessage = "Unable to find company main operatoe." };

        return new ApiResponse<MainOperatorDto>() { Data = _mapper.Map<MainOperatorDto>(opUser) };
    }
}
