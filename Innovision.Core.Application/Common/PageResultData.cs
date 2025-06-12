namespace Innovision.Core.Application.Common
{
    public class PaginateResult<T>
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<T> ListData { get; set; }
    }
}
