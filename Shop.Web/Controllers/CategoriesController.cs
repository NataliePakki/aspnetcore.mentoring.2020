using Microsoft.AspNetCore.Mvc;

using Shop.Web.Helpers;
using Shop.Core.Services;

namespace Shop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService service)
        {
            _categoryService = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(categories.ToViewModels());
        }
    }
}
