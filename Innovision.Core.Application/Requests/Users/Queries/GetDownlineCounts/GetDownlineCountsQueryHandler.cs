using AutoMapper;
using Innovision.Core.Application.Common;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Users.AgentAndPlayer.Queries.GetDownlineCounts;

public class GetDownlineCountsQueryHandler : IRequestHandler<GetDownlineCountsQuery, ApiResponse<object>>
{
    private readonly ICoreDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetDownlineCountsQueryHandler(ICoreDbContext dbContext, IMapper mapper, ICurrentUserService currentUserService)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<ApiResponse<object>> Handle(GetDownlineCountsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var account = await _dbContext.Accounts
                .Where(x => x.AccountObjectId == request.AccountObjectId)
                .FirstOrDefaultAsync(cancellationToken);

            var playersCount = await _dbContext.Accounts
                .Where(m => m.RefferralCode == account.RefferralKey && m.UserTypeId == UserTypes.Player).CountAsync();

            var agentCount = await _dbContext.Accounts
                .Where(m => m.RefferralCode == account.RefferralKey && m.UserTypeId == UserTypes.Agent).CountAsync();

            return new ApiResponse<object>()
            {
                Data = new { playersCount = playersCount, agentsCount = agentCount }
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<object>() { Success = false, ErrorMessage = ex.Message };
        }
    }
}
