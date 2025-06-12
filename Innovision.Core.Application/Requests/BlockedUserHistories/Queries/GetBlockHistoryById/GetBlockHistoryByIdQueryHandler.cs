using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Domain.Entity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockHistoryById;

public class GetBlockHistoryByIdQueryHandler : IRequestHandler<GetBlockHistoryByIdQuery, BlockUserDto>
{
  private readonly ICoreDbContext _coreDbContext;
  private readonly IMapper _mapper;

  public GetBlockHistoryByIdQueryHandler(ICoreDbContext dbContext, IMapper mapper)
  {
    _coreDbContext = dbContext;
    _mapper = mapper;
  }

  public async Task<BlockUserDto> Handle(GetBlockHistoryByIdQuery request, CancellationToken cancellationToken)
  {
    var blockHistory = await _coreDbContext.BlockedUserHistories
        .Include(b => b.Account)
        .Where(b => b.BlockedUserHistoryId == request.BlockedUserHistoryId)
        .FirstOrDefaultAsync(cancellationToken);
    _ = blockHistory ?? throw new EntityNotFoundException(typeof(BlockedUserHistory).Name, request.BlockedUserHistoryId);

    return _mapper.Map<BlockUserDto>(blockHistory);
  }
}
