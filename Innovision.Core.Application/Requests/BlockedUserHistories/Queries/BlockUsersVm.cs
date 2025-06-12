namespace Innovision.Core.Application.Requests.BlockedUserHistories.Queries.GetBlockedUsers;

public record BlockUsersVm(IEnumerable<BlockUserDto> BlockedUsers)
{
  public int Offset { get; set; }
  public int TotalCount { get; set; }
  public int PageSize { get; set; }
  public int Count
  {
    get
    {
      return BlockedUsers.Count();
    }
  }
}