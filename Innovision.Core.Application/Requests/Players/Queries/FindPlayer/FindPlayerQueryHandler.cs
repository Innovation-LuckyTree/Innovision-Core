using AutoMapper;
using AutoMapper.QueryableExtensions;
using Innovision.Core.Application.Common.Contants;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.Players.Queries.FindPlayer;

public class FindPlayerQueryHandler : IRequestHandler<FindPlayerQuery, PlayerDto>
{
    public readonly ICoreDbContext _coreDbContext;
    public readonly IMapper _mapper;

    public FindPlayerQueryHandler(ICoreDbContext coreDbContext, IMapper mapper)
    {
        _coreDbContext = coreDbContext;
        _mapper = mapper;
    }

    public async Task<PlayerDto> Handle(FindPlayerQuery request, CancellationToken cancellationToken)
    {
        var player = await _coreDbContext.Accounts
            .Include(o => o.Branch)
            .Where(o => o.UserId == request.UserId && o.UserTypeId == UserContants.USER_TYPE_PLAYER)
            .ProjectTo<PlayerDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (player == null)
            throw new EntityNotFoundException("Account", request);

        return player;
    }
}