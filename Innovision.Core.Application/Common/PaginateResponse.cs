namespace Innovision.Core.Application.Common
{
    public class PaginateResponse<T>
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public T Results { get; set; }
    }
}
