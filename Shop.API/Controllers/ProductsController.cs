using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
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

        [HttpGet("{id}")]
        public IActionResult Get(int id, bool includeAll = false)
        {
            var product = this._productService.Get(id, includeAll);
            if (product == null)
                return NotFound($"Product {id} was not found");
            return Ok(_mapper.Map<ProductModel>(product));
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
