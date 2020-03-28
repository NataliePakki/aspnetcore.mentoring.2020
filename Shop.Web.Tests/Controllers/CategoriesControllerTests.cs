using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;

using Shop.Core.Models;
using Shop.Core.Services;
using Shop.Web.Controllers;
using Shop.Web.Helpers;
using Shop.Web.ViewModels;

namespace Shop.Web.Tests.Controllers
{
    public class CategoriesControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfCategories()
        {
            // Arrange
            var categories = GetCategories();
            var viewModels = categories.ToViewModels();
            var mockService = new Mock<ICategoryService>();
            mockService.Setup(s => s.GetAll()).Returns(categories);

            var controller = new CategoriesController(mockService.Object);

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
