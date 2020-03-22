using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Core.Models
{
    [Table("dbo.Categories")]
    public class Category
    { 
        [Key]
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }
    }
}
