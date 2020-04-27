using System;
using System.ComponentModel.DataAnnotations;

namespace Shop.API.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        [Required(ErrorMessage = "The product name is required")]
        [MaxLength(30, ErrorMessage = "The product name is less than 30")]
        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public double UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public CategoryModel Category { get; set; }

        public SupplierModel Supplier { get; set; }

        public int CategoryID { get; set; }

        public int SupplierID { get; set; }
    }
}
