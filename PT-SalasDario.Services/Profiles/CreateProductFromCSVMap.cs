using CsvHelper.Configuration;
using PT_SalasDario.Services.Requests;

namespace PT_SalasDario.Services.Profiles
{
    public class CreateProductFromCSVMap : ClassMap<CreateProductFromCSV>
    {
        public CreateProductFromCSVMap()
        {
          
            Map(m => m.Code).Name("Product Code")
                .Validate(args => !string.IsNullOrWhiteSpace(args.Field));

            Map(m => m.Name).Name("Product Name")
                .Validate(args => !string.IsNullOrWhiteSpace(args.Field));

            Map(m => m.CategoryCode).Name("Category Code")
                .Validate(args => !string.IsNullOrWhiteSpace(args.Field));

            Map(m => m.CategoryName).Name("Category Name")
                .Validate(args => !string.IsNullOrWhiteSpace(args.Field));
        }
    }
}
