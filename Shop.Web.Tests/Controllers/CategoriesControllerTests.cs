using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

using Shop.Core.Models;
using Shop.Core.Services;
using Shop.Web.Controllers;
using Shop.Web.ViewModels;
using AutoMapper;
using Shop.Web.Models;

namespace Shop.Web.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        private CategoriesController controller;

        private Mock<ICategoryService> mockService;

        private IMapper mapper;

        public CategoriesControllerTests()
        {
            mockService = new Mock<ICategoryService>();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ViewMappingProfile());
            });
            mapper = config.CreateMapper();

            controller = new CategoriesController(mockService.Object, mapper);
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfCategories()
        {
            // Arrange
            var categories = GetCategories();
            var viewModels = mapper.Map<IEnumerable<CategoryViewModel>>(categories);

            mockService.Setup(s => s.GetAll(false)).Returns(categories);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }

        private IList<Category> GetCategories()
        {
            return new List<Category>()
            {
                new Category() {CategoryID = 1 }
            };
        }
    }
}
