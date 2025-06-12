using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.BlockedUserHistories.Queries;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Commands.UnblockUser;

public class UnblockUserCommandHandler : IRequestHandler<UnblockUserCommand, BlockUserDto>
{
  private readonly ICoreDbContext _coreDbContext;
  private readonly IMapper _mapper;

  public UnblockUserCommandHandler(ICoreDbContext coreDbContext, IMapper mapper)
  {
    _coreDbContext = coreDbContext;
    _mapper = mapper;
  }

  public async Task<BlockUserDto> Handle(UnblockUserCommand request, CancellationToken cancellationToken)
  {
    // get the user account
    var account = await _coreDbContext.Accounts
        .Where(o => o.AccountInfoId == request.AccountInfoId)
        .FirstOrDefaultAsync(cancellationToken);
    _ = account ?? throw new EntityNotFoundException(typeof(Account).Name, request.AccountInfoId);

    var activeBlockHistory = await _coreDbContext.BlockedUserHistories
        .Where(b => b.AccountInfoId == request.AccountInfoId && b.IsActive == 1)
        .FirstOrDefaultAsync(cancellationToken);
    _ = activeBlockHistory ?? throw new EntityNotFoundException(typeof(BlockedUserHistory).Name, request.AccountInfoId);

    account.AccountStatusId = AccountStatus.Completed;
    _coreDbContext.Accounts.Update(account);

    activeBlockHistory.IsActive = 0;
    _coreDbContext.BlockedUserHistories.Update(activeBlockHistory);

    await _coreDbContext.SaveChangesAsync(cancellationToken);

    return _mapper.Map<BlockUserDto>(activeBlockHistory);
  }
}
