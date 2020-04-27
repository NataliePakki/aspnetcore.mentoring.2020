using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using Shop.Core.Models;
using Shop.Core.Services;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeAll = false)
        {
            var products = _mapper.Map<IEnumerable<ProductModel>>(this._productService.GetAll(includeAll));
            return Ok(products);
        }

        [HttpGet("{id}", Name = "ProductGet")]
        public IActionResult Get(int id, bool includeAll = false)
        {
            var product = this._productService.Get(id, includeAll);
            if (product == null)
                return NotFound($"Product {id} was not found");
            return Ok(_mapper.Map<ProductModel>(product));
        }

        [HttpPost]
        public IActionResult Add([FromBody]ProductModel productModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var product = _mapper.Map<Product>(productModel);
            try
            {
                var newProduct = this._productService.Create(product);
                var url = this.Url.Link("ProductGet", new { id = newProduct.ProductID, includeAll = true });
                return Created(url, _mapper.Map<ProductModel>(newProduct));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = this._productService.Get(id);
            if (product == null)
                return NotFound($"Product {id} was not found");
            this._productService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductModel productModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var oldProduct = this._productService.Get(id);
            if (oldProduct == null)
                return NotFound($"Product {id} was not found");

            var updProduct = _mapper.Map<Product>(productModel);
            oldProduct.ProductName = updProduct.ProductName ?? oldProduct.ProductName;
            oldProduct.QuantityPerUnit = updProduct.QuantityPerUnit ?? oldProduct.QuantityPerUnit;
            oldProduct.UnitPrice = updProduct.UnitPrice != 0 ? updProduct.UnitPrice : oldProduct.UnitPrice;
            oldProduct.UnitsInStock = updProduct.UnitsInStock != 0 ? updProduct.UnitsInStock : oldProduct.UnitsInStock;

            this._productService.Update(oldProduct);
            return Ok(oldProduct);
        }

        [HttpGet("{id}/category")]
        public IActionResult GetCategory(int id)
        {
            var product = this._productService.Get(id, true);
            if (product == null)
                return NotFound($"Product {id} was not found");
            var category = _mapper.Map<CategoryModel>(product.Category);
            return Ok(category);
        }

        [HttpGet("{id}/supplier")]
        public IActionResult GetSupplier(int id)
        {
            var product = this._productService.Get(id, true);
            if (product == null)
                return NotFound($"Product {id} was not found");
            var supplier = _mapper.Map<SupplierModel>(product.Supplier);
            return Ok(supplier);
        }
    }
}
