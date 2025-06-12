namespace Innovision.Core.Infrastructure.Games.Models.Responses
{
    public class BetScheduleResponse
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public  DateTimeOffset Date { get; set; }
        public TimeSpan DrawTime { get; set; }
        public TimeSpan OpenSchedule { get; set; }
        public TimeSpan EndCutOff { get; set; }
        public int Status { get; set; }
        public bool IsDeleted { get; set; }
        public int GameDrawType { get; set; }
        public int CompanyGame { get; set; }
    }
}
