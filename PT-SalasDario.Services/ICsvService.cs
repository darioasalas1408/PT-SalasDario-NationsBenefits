using PT_SalasDario.Services.Requests;

namespace PT_SalasDario.Services
{
    public interface ICsvService
    {
        public Task<List<CreateProductFromCSV>> ReadCsv (string csvFilePath);
    }
}
