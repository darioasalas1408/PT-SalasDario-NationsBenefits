using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PT_SalasDario.Data
{
    [Table("category")]
    [Index(nameof(Code), IsUnique = true)]
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public DateTime CreationDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
