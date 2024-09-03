namespace CashFlow.Communication.Responses.Pagination;
public record ResponsePaginationJson
{
    public int Page { get; init; }
    public int PageSize { get; init; }
    public int Total { get; init; }
    public int TotalPages { get; init; }
}
