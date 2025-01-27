namespace Base.Domain.Shared;

public record PaginatedResponse<T>
{
    public long PageSize { get; init; }
    public long PageCount { get; init; }
    public long TotalItemsCount { get; init; }
    public required IEnumerable<T> Data { get; init; }
}
