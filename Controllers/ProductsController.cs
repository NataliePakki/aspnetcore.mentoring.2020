
using System.Linq;
using AspNetCoreMentoring.Helpers;
using AspNetCoreMentoring.Models;
using AspNetCoreMentoring.Services;
using AspNetCoreMentoring.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreMentoring.Controllers
{ 
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private readonly ISupplierService _supplierService;
        private readonly int maxCount;

        public ProductsController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService, IConfiguration configuration)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
            maxCount = (int)configuration.GetValue(typeof(int), "MaxCountProducts");
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll();
            if (maxCount > 0)
            {
                products = products.Take(maxCount);
            }
            return View(products.ToViewModels());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new Product();
            var viewModel = product.ToCreateViewModel(_categoryService.GetAll(),_supplierService.GetAll());
  
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var newProduct = viewModel.ToProduct();
                _productService.Create(newProduct);

                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = _categoryService.GetAll().ToSelectList();
            viewModel.Suppliers = _supplierService.GetAll().ToSelectList();
            return View(viewModel);     
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var viewModel = product.ToEditViewModel(_categoryService.GetAll(), _supplierService.GetAll());

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var updateProduct = viewModel.ToProduct();
                _productService.Update(updateProduct);

                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = _categoryService.GetAll().ToSelectList();
            viewModel.Suppliers = _supplierService.GetAll().ToSelectList();
            return View(viewModel);
        }
    }
}
