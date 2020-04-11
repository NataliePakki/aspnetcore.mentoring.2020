using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.Core.Services;

namespace Shop.Web.Helpers
{
    public class FormDataHelper: IFormDataHelper
    {
        private readonly ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;


        public FormDataHelper(ICategoryService categoryService, ISupplierService supplierService)
        {
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public IEnumerable<SelectListItem> GetCategories()
        {
            return _categoryService.GetAll().ToSelectList();
        }

        public IEnumerable<SelectListItem> GetSuppliers()
        {
            return _supplierService.GetAll().ToSelectList();
        }
    }
}
