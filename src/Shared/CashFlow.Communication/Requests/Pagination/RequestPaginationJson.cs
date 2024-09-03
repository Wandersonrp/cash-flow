namespace CashFlow.Communication.Requests.Pagination;
public record RequestPaginationJson
{
    public int Page { get; set; }
    public int ItemsPerPage { get; set; }
}
