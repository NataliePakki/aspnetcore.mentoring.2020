using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreMentoring.Helpers;
using AspNetCoreMentoring.Models;
using AspNetCoreMentoring.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreMentoring.Controllers
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
