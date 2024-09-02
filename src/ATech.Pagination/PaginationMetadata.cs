namespace ATech.Pagination;

public record PaginationMetadata(long TotalCount, int PageSize, int CurrentPage, int TotalPages, bool HasNext, bool HasPrevious);
