using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Web.Helpers
{
    public interface IFormDataHelper
    {
        IEnumerable<SelectListItem> GetSuppliers();

        IEnumerable<SelectListItem> GetCategories();
    }
}
