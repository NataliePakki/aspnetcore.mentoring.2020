using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}/image")]
        [Produces("image/jpeg")]
        public IActionResult GetImage(int id)
        {
            var category = this._categoryService.Get(id, true);
            if (category == null)
                return NotFound($"Category {id} was not found");

            return File(category.Picture, "image/jpeg");
        }

        [HttpPut("{id}/image")]
        public IActionResult UpdateImage(int id, IFormFile file)
        {
            var category = this._categoryService.Get(id, true);
            if (category == null)
                return NotFound($"Category {id} was not found");
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    category.Picture = memoryStream.ToArray();
                }

                this._categoryService.Update(category);

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
