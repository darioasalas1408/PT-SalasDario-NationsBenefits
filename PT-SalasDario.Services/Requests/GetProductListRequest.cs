namespace PT_SalasDario.Services.Requests
{
    public class GetProductListRequest
    {
        public string ProductCode { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; } 
    }
}
