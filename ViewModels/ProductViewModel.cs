using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreMentoring.ViewModels
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Quantity")]
        public string QuantityPerUnit { get; set; }

        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        [DisplayName("In Stock")]
        public int UnitsInStock { get; set; }

        [DisplayName("Category name")]
        public string CategoryName { get; set; }

        [DisplayName("Supplier name")]
        public string SupplierName { get; set; }
    }
}
