using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;

using Shop.Web.Helpers;
using Shop.Core.Services;
using Shop.Web.ViewModels;

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

        [HttpGet]
        [Route("images/{id}")]
        public IActionResult Image(int id)
        {

            var category = _categoryService.Get(id);
            if(category == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var image = category.Picture;
            return File(image, "image/jpeg");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryService.Get(id);
            if(category == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var editViewModel = category.ToEditViewModel();
            return View(editViewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Result = "Please correct the form.";

                return View(model);
            }

            if (model.FormFile != null)
            {
                var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                    model.FormFile.FileName);

                if (model.FormFile.Length == 0)
                {
                    ModelState.AddModelError(model.FormFile.Name,
                        $"{trustedFileNameForDisplay} is empty.");

                    return View(model);
                }

                using (var memoryStream = new MemoryStream())
                {
                    model.FormFile.CopyTo(memoryStream);

                    if (memoryStream.Length == 0)
                    {
                        ModelState.AddModelError(model.FormFile.Name,
                            $"{trustedFileNameForDisplay} is empty.");
                        return View(model);
                    }

                    model.FileContent = memoryStream.ToArray();
                }
            }

            _categoryService.Update(model.ToCategory());

            return RedirectToAction(nameof(Index));
        }
    }
}
