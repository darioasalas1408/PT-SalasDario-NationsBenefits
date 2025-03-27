using Microsoft.EntityFrameworkCore;
using PT_SalasDario.Data;

namespace PT_SalasDario.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MyDbContext _dbContext;

        public ProductRepository(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateOrUpdateProducts(List<Product> products)
        {
            var productCodes = products.Select(p => p.Code).ToList();
            var categoryCodes = products.Select(p => p.CategoryCode).Distinct().ToList();

            var existingCategories = await _dbContext.Categories
                .Where(c => categoryCodes.Contains(c.Code))
                .ToDictionaryAsync(c => c.Code);

            var existingProducts = await _dbContext.Products
                .Where(p => productCodes.Contains(p.Code))
                .ToDictionaryAsync(p => p.Code);

            foreach (var product in products)
            {
                if (existingCategories.TryGetValue(product.CategoryCode, out var existingCategory))
                {
                    if (existingCategory.Name != product.Category.Name)
                    {
                        existingCategory.Name = product.Category.Name;
                    }
                    product.Category = existingCategory; 
                }
                else
                {
                    _dbContext.Categories.Add(product.Category); 
                    existingCategories[product.CategoryCode] = product.Category;
                }

                if (existingProducts.TryGetValue(product.Code, out var existingProduct))
                {
                    bool updateNeeded = false;

                    if (existingProduct.Name != product.Name)
                    {
                        existingProduct.Name = product.Name;
                        updateNeeded = true;
                    }

                    if (existingProduct.CategoryCode != product.CategoryCode)
                    {
                        existingProduct.CategoryCode = product.CategoryCode;
                        existingProduct.Category = product.Category;
                        updateNeeded = true;
                    }

                    if (updateNeeded)
                    {
                        _dbContext.Products.Update(existingProduct);
                    }
                }
                else
                {
                    _dbContext.Products.Add(product); 
                }
            }

            await _dbContext.SaveChangesAsync(); 
        }

        public IQueryable<Product> GetProducts()
        {
            return _dbContext.Products;
        }
    }
}
