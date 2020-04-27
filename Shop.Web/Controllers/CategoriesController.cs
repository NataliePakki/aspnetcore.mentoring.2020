using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.IO;

using Shop.Core.Services;
using Shop.Web.ViewModels;
using AutoMapper;
using Shop.Core.Models;
using System.Collections.Generic;

namespace Shop.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoriesController(ICategoryService service, IMapper mapper)
        {
            _categoryService = service;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var categories = _categoryService.GetAll();
            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        [HttpGet]
        [Produces("image/jpeg")]
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
            var editViewModel = _mapper.Map<EditCategoryViewModel>(category);
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

            _categoryService.Update(_mapper.Map<Category>(model));

            return RedirectToAction(nameof(Index));
        }
    }
}
