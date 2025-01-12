namespace Ykotika.WebAPI.Models
{
    public class BaseListQuery
    {
        public string? SortBy { get; set; }
        public bool IsDescending { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}
