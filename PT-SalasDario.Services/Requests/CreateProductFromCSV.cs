namespace PT_SalasDario.Services.Requests
{
    public class CreateProductFromCSV
    {
        public string Code { get; set; }

        public string Name { get; set; }
        
        public string CategoryCode { get; set; }
        
        public string CategoryName { get; set; }
    }
}
