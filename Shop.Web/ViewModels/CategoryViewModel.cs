using System.ComponentModel;

namespace Shop.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
