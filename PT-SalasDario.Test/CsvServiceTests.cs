using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT_SalasDario.Services;

namespace PT_SalasDario.Test
{
    [TestClass]
    public class CsvServiceTests
    {
        private CsvService _csvService;

        [TestInitialize]
        public void Setup()
        {
            _csvService = new CsvService();
        }

        [TestMethod]
        public async Task ReadCsv_ShouldReturnValidRecords_WhenCsvIsValid()
        {
            // Arrange
            var csvContent = @"Product Code,Product Name,Category Code,Category Name
P001,Product1,c3d3225c-f662-47e5-9c77-66926d1fb351,Category 1
P002,Product2,88268184-54c6-44f1-b62f-b1c07e91ac22,Category 2";

            var tempFilePath = Path.Combine(Path.GetTempPath(), "test.csv");
            await File.WriteAllTextAsync(tempFilePath, csvContent);

            // Act
            var result = await _csvService.ReadCsv(tempFilePath);

            // Assert
            result.Should().HaveCount(2);
            result[0].Code.Should().Be("P001");
            result[0].Name.Should().Be("Product1");
            result[1].Code.Should().Be("P002");
            result[1].Name.Should().Be("Product2");

            // Clean up
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task ReadCsv_ShouldThrowException_WhenHeadersAreMissing()
        {
            // Arrange
            var csvContent = @"Product Code,Product Name
P001,Product1
P002,Product2";

            var tempFilePath = Path.Combine(Path.GetTempPath(), "test_missing_headers.csv");
            await File.WriteAllTextAsync(tempFilePath, csvContent);

            // Act
            Func<Task> act = async () => await _csvService.ReadCsv(tempFilePath);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                     .WithMessage("The following headers are missing in the CSV file: Category Code, Category Name");

            // Clean up
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task ReadCsv_ShouldThrowException_WhenCsvIsEmpty()
        {
            // Arrange
            var emptyCsvContent = "";
            var tempFilePath = Path.Combine(Path.GetTempPath(), "empty.csv");
            await File.WriteAllTextAsync(tempFilePath, emptyCsvContent);

            // Act
            Func<Task> act = async () => await _csvService.ReadCsv(tempFilePath);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                     .WithMessage("Could not read the headers from the CSV file.");

            // Clean up
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task ReadCsv_ShouldHandleValidationErrors_WhenInvalidCsvData()
        {
            // Arrange
            var invalidCsvContent = @"Product Code,Product Name,Category Code,Category Name
48f66864-2e70-41be-ac33-403be2fb0c70,Product A,c3d3225c-f662-47e5-9c77-66926d1fb351,Category 1
INVALID ROW";

            var tempFilePath = Path.Combine(Path.GetTempPath(), "test_invalid_data.csv");
            await File.WriteAllTextAsync(tempFilePath, invalidCsvContent);

            // Act
            var result = await _csvService.ReadCsv(tempFilePath);

            // Assert
            result.Should().HaveCount(1);
            result[0].Code.Should().Be("48f66864-2e70-41be-ac33-403be2fb0c70");

            // Clean up
            File.Delete(tempFilePath);
        }
    }
}