using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;

namespace PT_SalasDario.Services
{
    public interface IProductService
    {
        Task<Tuple<List<ProductListResponseDTO>, int, int, int, int>> GetProducts(GetProductListRequest request);

        public Task<List<CreateProductFromCSV>> ImportProductFromCsv(List<CreateProductFromCSV> listCreateProductFromCSV);
    }
}
