namespace PT_SalasDario.API.Requests
{
    public class GetProductListRequest : GetPaginatedListBaseRequest
    {
        public string? ProductCode { get; set; }
    }
}
