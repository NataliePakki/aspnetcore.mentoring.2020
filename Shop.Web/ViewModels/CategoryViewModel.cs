using System.ComponentModel;

namespace Shop.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }
}
