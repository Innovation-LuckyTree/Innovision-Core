using AutoMapper;
using Innovision.Core.Application.Exceptions;
using Innovision.Core.Application.Interfaces;
using Innovision.Core.Application.Requests.BlockedUserHistories.Queries;
using Innovision.Core.Domain.Entity;
using Innovision.Core.Domain.Enums;
using Innovision.Core.Infrastructure.Interfaces;
using Innovision.Core.Infrastructure.WebsocketServices.Models.Requests;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Innovision.Core.Application.Requests.BlockedUserHistories.Commands.BlockUser;

public class BlockUserCommandHandler(ICoreDbContext coreDbContext, IMapper mapper, IWebsocketServicesApi websocketServiceApi) : IRequestHandler<BlockUserCommand, BlockUserDto>
{
  private readonly ICoreDbContext _coreDbContext = coreDbContext;
  private readonly IMapper _mapper = mapper;
  private readonly IWebsocketServicesApi _websocketServiceApi = websocketServiceApi;

  public async Task<BlockUserDto> Handle(BlockUserCommand request, CancellationToken cancellationToken)
  {
    // get user account
    var account = await _coreDbContext.Accounts
      .Where(o => o.AccountInfoId == request.AccountInfoId)
      .FirstOrDefaultAsync(cancellationToken);

    _ = account ?? throw new EntityNotFoundException(typeof(Account).Name, request.AccountInfoId);

    // check if the user is already blocked
    var existingActiveBlock = await _coreDbContext.BlockedUserHistories
      .AnyAsync(b => b.AccountInfoId == request.AccountInfoId && b.IsActive == 1, cancellationToken);

    if (existingActiveBlock)
    {
      throw new UserAlreadyBlockedException($"{account.FirstName} {account.LastName}");
    }

    // update user status to blocked
    account.AccountStatusId = AccountStatus.Block;
    _coreDbContext.Accounts.Update(account);
    await _coreDbContext.SaveChangesAsync(cancellationToken);

    // add to BlockedUserHistory
    var blockedUserHistory = new BlockedUserHistory
    {
      AccountInfoId = request.AccountInfoId,
      Remarks = request.Remarks,
      BlockedDate = DateTime.UtcNow,
      IsActive = 1
    };

    _coreDbContext.BlockedUserHistories.Add(blockedUserHistory);
    await _coreDbContext.SaveChangesAsync(cancellationToken);

    await NotifyAccount(request.AccountInfoId, cancellationToken);

    return _mapper.Map<BlockUserDto>(blockedUserHistory);
  }

  private async Task NotifyAccount(long accountId, CancellationToken cancellationToken)
  {
    await Task.Run(async () => await _websocketServiceApi.BlockUser(new BlockUserRequest(accountId), cancellationToken), cancellationToken);
  }
}
