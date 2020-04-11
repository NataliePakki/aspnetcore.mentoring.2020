using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Components
{
    [ViewComponent]
    public class Breadcrumb: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()     
        {
            var controllerName = ViewContext.RouteData.Values["controller"].ToString();
            var actionName = ViewContext.RouteData.Values["action"].ToString();
            return View(new Tuple<string, string>(controllerName, actionName));
        }
    }
}
