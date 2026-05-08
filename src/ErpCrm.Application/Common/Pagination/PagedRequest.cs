namespace ErpCrm.Application.Common.Pagination;
public class PagedRequest
{
    private const int MaxPageSize = 200;
    public int Page { get; set; } = 1;
    private int _pageSize = 20;
    public int PageSize { get => _pageSize; set => _pageSize = value > MaxPageSize ? MaxPageSize : value <= 0 ? 20 : value; }
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
    public int Skip => Page <= 1 ? 0 : (Page - 1) * PageSize;
}
