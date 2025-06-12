using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, PlayerAccountDto>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IMapper _mapper;
    private readonly ICoreDbContext _dbContext;

    public GetCurrentUserQueryHandler(ICurrentUserService currentUserService, IMapper mapper, ICoreDbContext dbContext)
    {
        _currentUserService = currentUserService;
        _mapper = mapper;
        _dbContext = dbContext;
    }

    public async Task<PlayerAccountDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.UserId == _currentUserService.UserObjId && m.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .ProjectTo<PlayerAccountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = userInfo ?? throw new EntityNotFoundException("Account", _currentUserService.UserId);

        return userInfo;
    }
}