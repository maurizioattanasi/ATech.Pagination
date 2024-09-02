namespace ATech.Pagination;

public class PagingParameters
{
const int maxPageSize = int.MaxValue;

    /// <summary>
    /// First page
    /// </summary>
    public int PageNumber { get; set; } = 1;

    private int _pageSize = maxPageSize;
    /// <summary>
    /// Items per page
    /// </summary>
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value > maxPageSize ? maxPageSize : value;
        }
    }
}
