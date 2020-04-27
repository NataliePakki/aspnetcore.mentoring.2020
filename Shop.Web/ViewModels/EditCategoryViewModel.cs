using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.ViewModels
{
    public class EditCategoryViewModel
    {
        [HiddenInput]
        public int CategoryID { get; set; }

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [HiddenInput]
        public string Description { get; set; }

        [DisplayName("Picture")]
        public IFormFile FormFile { get; set; }

        [HiddenInput]
        public byte[] FileContent { get; set; }

        public string Result { get; set; }
    }
}
