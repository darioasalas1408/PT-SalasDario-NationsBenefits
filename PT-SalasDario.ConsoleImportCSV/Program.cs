using PT_SalasDario.ConsoleImportCSV;
using PT_SalasDario.ConsoleImportCSV.Handlers;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    //Business logic suggestion: We can change this manual process by a Background worker that is constantly verifying if there's any file in a directory. If so, products are processed
    static async Task Main(string[] args)
    {
        string filePath;

        if (args.Length > 0)
        {
            filePath = args[0];
        }
        else
        {
            Console.Write("Enter the absolute path of the CSV file: ");
            filePath = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
        {
            Console.WriteLine("File not found.");
            return;
        }

        await ImportProductFromCsv(filePath);
    }

    private static async Task ImportProductFromCsv(string filePath)
    {
        var serviceProvider = Startup.ConfigureServices();
        var importHandler = serviceProvider.GetRequiredService<IImportHandler>();

        await importHandler.ImportProductFromCsv(filePath);
    }
}

