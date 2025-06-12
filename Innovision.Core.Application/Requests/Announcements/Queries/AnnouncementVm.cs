namespace Innovision.Core.Application.Requests.Announcements.Queries;

public record AnnouncementVm(IEnumerable<AnnouncementDto> Announcements)
{
  public int Offset { get; set; }
  public int TotalCount { get; set; }
  public int PageSize { get; set; }
  public int Count
  {
    get
    {
      return Announcements.Count();
    }
  }
}