using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public interface IProductRepository
    {
        Task CreateOrUpdateProducts(List<Product> products);

        IQueryable<Product> GetProducts();
    }
}
