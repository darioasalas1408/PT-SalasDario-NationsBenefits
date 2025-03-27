using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PT_SalasDario.Data
{
    [Table("product")]
    [Index(nameof(Code), IsUnique = true)]
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string CategoryCode { get; set; }

        public DateTime CreationDate { get; set; }

        public Category Category { get; set; }
    }
}
