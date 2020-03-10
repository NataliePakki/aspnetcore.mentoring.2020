using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AspNetCoreMentoring.ViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "The product name is required")]
        [MaxLength(30, ErrorMessage = "The product name is less than 30")]
        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        [DisplayName("Quantity")]
        public string QuantityPerUnit { get; set; }

        [DisplayName("Price")]
        public double UnitPrice { get; set; }

        [MinLength(1, ErrorMessage = "At least one is required")]
        [DisplayName("In Stock")]
        public int UnitsInStock { get; set; }

        [DisplayName("Category")]
        public int CategoryID { get; set; }

        [DisplayName("Supplier")]
        public int SupplierID { get; set; }

        [DisplayName("Category")]
        public IList<SelectListItem> Categories { get; set; }

        [DisplayName("Suppliers")]
        public IList<SelectListItem> Suppliers { get; set; }

    }
}
