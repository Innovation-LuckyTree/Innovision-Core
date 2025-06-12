using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Accounts.Queries.GetCurrentSystemUser;

public class GetCurrentSystemUserQueryHandler(IMapper mapper, ICoreDbContext dbContext, ICurrentUserService currentUserService) : IRequestHandler<GetCurrentSystemUserQuery, SystemAccountInfoDto>
{
    private readonly ICurrentUserService _currentUserService = currentUserService;
    private readonly IMapper _mapper = mapper;
    private readonly ICoreDbContext _dbContext = dbContext;

    public async Task<SystemAccountInfoDto> Handle(GetCurrentSystemUserQuery request, CancellationToken cancellationToken)
    {
        var userInfo = await _dbContext.Accounts
            .Include(m => m.Branch)
            .Where(m => m.UserId == _currentUserService.UserObjId && m.UserTypeId != UserContants.USER_TYPE_PLAYER)
            .ProjectTo<SystemAccountInfoDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        _ = userInfo ?? throw new EntityNotFoundException("Account", _currentUserService.UserId);

        return userInfo;
    }
}