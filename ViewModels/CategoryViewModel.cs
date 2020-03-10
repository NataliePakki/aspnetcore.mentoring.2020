using System;
using System.ComponentModel;

namespace AspNetCoreMentoring.ViewModels
{
    public class CategoryViewModel
    {
        [DisplayName("Category Name")]
        public string Name { get; set; }
    }
}
