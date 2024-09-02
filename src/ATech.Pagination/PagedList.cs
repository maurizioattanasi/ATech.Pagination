using System.Linq.Expressions;

namespace ATech.Pagination;

public class PagedList<T> : List<T>
{
    public PagedList(List<T> items, long count, int page, int pageSize)
    {
        TotalCount = count;
        CurrentPage = page;
        PageSize = pageSize;

        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        if (items.Any())
            AddRange(items);
    }

    public long TotalCount { get; }
    public int CurrentPage { get; }
    public int PageSize { get; }
    public int TotalPages { get; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize, Expression<Func<T, dynamic>> orderBy = null!, bool ascending = true)
    {
        var count = source.Count();
        var items = source;

        if (orderBy is not null)
            items = ascending ? items.OrderBy(orderBy) : items.OrderByDescending(orderBy);

        items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return new PagedList<T>(items.ToList(), count, pageNumber, pageSize);
    }

    public static PagedList<T> Empty => new PagedList<T>(new List<T>(), 0, 0, 0);

    public PaginationMetadata PaginationMetadata
        => new PaginationMetadata(TotalCount, PageSize, CurrentPage, TotalPages, HasNext, HasPrevious);
}