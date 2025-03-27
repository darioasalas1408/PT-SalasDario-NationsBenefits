using CsvHelper;
using CsvHelper.Configuration;
using PT_SalasDario.Services.Profiles;
using PT_SalasDario.Services.Requests;
using System.Globalization;

namespace PT_SalasDario.Services
{
    public class CsvService : ICsvService
    {
        public async Task<List<CreateProductFromCSV>> ReadCsv(string csvFilePath)
        {
            using var reader = new StreamReader(csvFilePath);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = (headerArgs) =>
                {
                    throw new Exception(message: $"Invalid headers detected: {headerArgs.InvalidHeaders}");
                },
                MissingFieldFound = (missingArgs) =>
                {
                    throw new Exception($"Missing field detected: {missingArgs.HeaderNames?.FirstOrDefault()}");
                }
            };

            using var csv = new CsvReader(reader, config);

            if (!csv.Read() || !csv.ReadHeader())
            {
                throw new Exception("Could not read the headers from the CSV file.");
            }

            var headerRecord = csv.HeaderRecord ?? Array.Empty<string>();

            var expectedHeaders = new[] { "Product Code", "Product Name", "Category Code", "Category Name" };
            var missingHeaders = expectedHeaders.Except(headerRecord, StringComparer.OrdinalIgnoreCase).ToList();

            if (missingHeaders.Any())
            {
                throw new Exception($"The following headers are missing in the CSV file: {string.Join(", ", missingHeaders)}");
            }

            csv.Context.RegisterClassMap<CreateProductFromCSVMap>();

            var validRecords = new List<CreateProductFromCSV>();

            try
            {
                while (await csv.ReadAsync())
                {
                    try
                    {
                        var record = csv.GetRecord<CreateProductFromCSV>(); 
                        validRecords.Add(record);
                    }
                    catch (CsvHelper.ValidationException ex)
                    {
                        Console.WriteLine($"Validation error on row {csv.Context}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Unexpected error on row {csv.Context}: {ex.Message}");
                    }
                }
            }
            catch (CsvHelper.ValidationException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}"); //TODO: We can implement some extra logging
            }

            return validRecords;
        }
    }
}
