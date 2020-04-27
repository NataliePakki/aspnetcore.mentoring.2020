using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Shop.Core.Models;
using Shop.Web.Helpers;
using Shop.Core.Services;
using Shop.Web.ViewModels;
using Shop.Web.Models;
using Shop.Web.Filters;
using AutoMapper;

namespace Shop.Web.Controllers
{
    [LogAction(true)]
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private readonly int maxCount;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IOptions<Settings> optionSettings, IMapper mapper)
        {
            _productService = productService;
            maxCount = optionSettings.Value.MaxCountProducts;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAll(true);
            if (maxCount > 0)
            {
                products = products.Take(maxCount);
            }
            var viewModels = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            return View(viewModels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var product = new Product();
            var viewModel = _mapper.Map<CreateProductViewModel>(product);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newProduct = _mapper.Map<Product>(viewModel);
                _productService.Create(newProduct);

                return RedirectToAction(nameof(Index));
            }

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

            var viewModel = _mapper.Map<EditProductViewModel>(product);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(EditProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var updateProduct = _mapper.Map<Product>(viewModel);
                _productService.Update(updateProduct);

                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
    }
}
