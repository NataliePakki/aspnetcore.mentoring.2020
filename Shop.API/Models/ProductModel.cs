using System;
namespace Shop.API.Models
{
    public class ProductModel
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }

        public string QuantityPerUnit { get; set; }

        public double UnitPrice { get; set; }

        public int UnitsInStock { get; set; }

        public CategoryModel Category { get; set; }

        public SupplierModel Supplier { get; set; }
    }
}
