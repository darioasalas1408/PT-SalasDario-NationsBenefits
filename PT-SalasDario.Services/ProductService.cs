using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services.Requests;
using PT_SalasDario.Services.Response;

namespace PT_SalasDario.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<Tuple<List<ProductListResponseDTO>, int, int, int, int>> GetProducts(GetProductListRequest request)
        {
            var query = _productRepository.GetProducts()
                .Where(w => request.ProductCode == null || w.Code.Contains(request.ProductCode))
                .OrderBy(o => o.Name);

            var products = await query
                .Include(i => i.Category)
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductListResponseDTO
                {
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Category = x.Category.Name,
                    Code = x.Code
                })
                .ToListAsync();

            var totalCount = await query.CountAsync();
            var totalPages = Math.Ceiling((double)totalCount / request.PageSize);

            return new Tuple<List<ProductListResponseDTO>, int, int, int, int>(products, totalCount, (int)totalPages, request.Page, request.PageSize);
        }

        public async Task<List<CreateProductFromCSV>> ImportProductFromCsv(List<CreateProductFromCSV> listCreateProductFromCSV)
        {
            var validateProductToImportResult = await ValidateProductToImport(listCreateProductFromCSV);

            if (validateProductToImportResult.uniqueProducts.Any())
            {
                await _productRepository.CreateOrUpdateProducts(_mapper.Map<List<Product>>(validateProductToImportResult.uniqueProducts));
            }

            return validateProductToImportResult.repeatedProducts;
        }

        private async Task<(List<CreateProductFromCSV> uniqueProducts, List<CreateProductFromCSV> repeatedProducts)> ValidateProductToImport(List<CreateProductFromCSV> productsToImport)
        {
            var groupedProducts = productsToImport
                                    .GroupBy(p => new { p.CategoryCode, p.Code })
                                    .ToList();

            var repeatedProducts = groupedProducts
                                    .Where(g => g.Count() > 1)
                                    .SelectMany(g => g)
                                    .ToList();

            var uniqueProducts = groupedProducts
                                    .Select(g => g.First())
                                    .ToList();


            var repetGroup = repeatedProducts
                                    .GroupBy(p => new { p.CategoryCode, p.Code })
                                    .ToList();

            return (uniqueProducts, repetGroup.Select(g => g.First()).ToList());
        }

    }
}
