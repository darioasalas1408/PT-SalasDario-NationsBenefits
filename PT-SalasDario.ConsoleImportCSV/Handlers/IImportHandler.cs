namespace PT_SalasDario.ConsoleImportCSV.Handlers
{
    public interface IImportHandler
    {
        public Task ImportProductFromCsv(string filePath);
    }
}
