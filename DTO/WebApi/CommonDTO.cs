namespace DTO.WebApi
{
    public class PageListRequest
    {
        public int rowCount { get; set; } = 5;
        public int page { get; set; } = 0;
        public string sort { get; set; }
        public string sortDirection { get; set; }
    }

    public class PageList<T>
    {
        public dynamic itemCount { get; set; }
        public T items { get; set; }
    }

    public enum SortDirection
    {
        asc,
        desc
    }
}
