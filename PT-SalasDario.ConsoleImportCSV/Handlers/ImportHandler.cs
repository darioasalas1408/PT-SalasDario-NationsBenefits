using Microsoft.Extensions.DependencyInjection;
using PT_SalasDario.Services;

namespace PT_SalasDario.ConsoleImportCSV.Handlers
{
    public class ImportHandler : IImportHandler
    {
        private readonly ServiceProvider _serviceProvider ;
        private readonly IProductService _productService;
        private readonly ICsvService _csvService;

        public ImportHandler()
        {
            _serviceProvider = Startup.ConfigureServices();
            _productService = _serviceProvider.GetRequiredService<IProductService>();
            _csvService = _serviceProvider.GetRequiredService<ICsvService>();
        }

        public async Task ImportProductFromCsv(string filePath)
        {
            var productsFromCSV = await _csvService.ReadCsv(filePath);
            if (!productsFromCSV.Any())
            {
                return; 
            }

            var repeatedProducts = await _productService.ImportProductFromCsv(productsFromCSV);
            if (repeatedProducts.Count() > 0)
            {
                Console.WriteLine($"Products repeated: {repeatedProducts.Count}");
                foreach (var product in repeatedProducts)
                {
                    Console.WriteLine($"Product repeated: {product.Code}, {product.Name}");
                }
            }
        }
    }
}
