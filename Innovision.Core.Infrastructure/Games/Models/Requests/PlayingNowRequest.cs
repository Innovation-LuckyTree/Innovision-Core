namespace Innovision.Core.Infrastructure.Games.Models.Requests
{
    public class PlayingNowRequest
    {
        public Guid CompanyId { get; set; }
        public int Start { get; set; }
        public int Size { get; set; }
    }
}
