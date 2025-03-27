using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PT_SalasDario.ConsoleImportCSV.Handlers;
using PT_SalasDario.Data;
using PT_SalasDario.Repository;
using PT_SalasDario.Services;
using PT_SalasDario.Services.Profiles;

namespace PT_SalasDario.ConsoleImportCSV
{
    public static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMappingProfile>();
            });

            return new ServiceCollection()
                .AddSingleton<IConfiguration>(configuration)
                .AddDbContext<MyDbContext>(options =>
                {
                    var connectionString = configuration.GetConnectionString("DefaultConnection");
                    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                })
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<IImportHandler, ImportHandler>()
                .AddScoped<ICsvService, CsvService>()
                .AddSingleton<IMapper>(mapperConfig.CreateMapper())
                .BuildServiceProvider();
        }
    }
}
