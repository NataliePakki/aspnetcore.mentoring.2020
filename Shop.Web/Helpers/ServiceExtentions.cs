using System.Collections.Generic;
using System.Linq;
using Shop.Core.Models;
using Shop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Web.Helpers
{
    public static class ServiceExtentions
    {
        public static IList<SelectListItem> ToSelectList(this IEnumerable<Category> categories)
        {
            if (categories == null)
                return null;
            return categories.Select(x => x.ToSelectListItem()).ToList();
        }

        public static SelectListItem ToSelectListItem(this Category category)
        {
            if (category == null)
                return null;
            return new SelectListItem()
            {
                Value = category.CategoryID.ToString(),
                Text = category.CategoryName
            };
        }


        public static IList<SelectListItem> ToSelectList(this IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
                return null;
            return suppliers.Select(x => x.ToSelectListItem()).ToList();
        }

        public static SelectListItem ToSelectListItem(this Supplier supplier)
        {
            if (supplier == null)
                return null;
            return new SelectListItem()
            {
                Value = supplier.SupplierID.ToString(),
                Text = supplier.CompanyName
            };
        }
    }
}
