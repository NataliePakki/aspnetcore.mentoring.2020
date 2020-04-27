using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Models;
using Shop.Core.Services;


namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _mapper.Map<IEnumerable<CategoryModel>>(this._categoryService.GetAll());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = this._categoryService.Get(id, true);
            if (category == null)
                return NotFound($"Category {id} was not found");
            return Ok(_mapper.Map<CategoryModel>(category));
        }
    }
}
