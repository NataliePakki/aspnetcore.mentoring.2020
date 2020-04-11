﻿using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Shop.Core.Models;
using Shop.Web.Helpers;
using Shop.Core.Services;
using Shop.Web.ViewModels;
using Shop.Web.Models;
using Shop.Web.Filters;

namespace Shop.Web.Controllers
{
    [LogAction(true)]
    public class ProductsController : Controller
    {
        private IProductService _productService;
        private readonly int maxCount;

        public ProductsController(IProductService productService, IOptions<Settings> optionSettings)
        {
            _productService = productService;
            maxCount = optionSettings.Value.MaxCountProducts;
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
            var viewModel = product.ToCreateViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newProduct = viewModel.ToProduct();
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

            var viewModel = product.ToEditViewModel();

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
            return View(viewModel);
        }
    }
}
