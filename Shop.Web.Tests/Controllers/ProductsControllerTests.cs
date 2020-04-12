using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Xunit;

using Shop.Core.Models;
using Shop.Core.Services;
using Shop.Web.Controllers;
using Shop.Web.Helpers;
using Shop.Web.ViewModels;
using Shop.Web.Models;

namespace Shop.Web.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private Mock<IProductService> mockProductsService;

        private ProductsController controller;

        public ProductsControllerTests()
        {
            mockProductsService = new Mock<IProductService>();

            IOptions<Settings> someOptions = Options.Create<Settings>(new Settings() { MaxCountProducts = 0});

            controller = new ProductsController(mockProductsService.Object, someOptions); ;
        }

        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfProducts()
        {
            // Arrange
            var products = GetProducts();
            var viewModels = products.ToViewModels();

            mockProductsService.Setup(s => s.GetAll()).Returns(products);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public void Create_ReturnsAViewResult_WithCreateProductView()
        { 
            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CreateProductViewModel>(
                viewResult.ViewData.Model);
        }

        [Fact]
        public void Create_RedirectToIndex()
        {
            // Arrange
            var newProduct = new CreateProductViewModel();

            // Act
            RedirectToActionResult redirectResult =
                   (RedirectToActionResult) controller.Create(newProduct);

            // Assert
            Assert.Equal(redirectResult.ActionName, "Index");
        }

        [Fact]
        public void Create_CreateProductCalled()
        {
            // Arrange
            var newProduct = new CreateProductViewModel();

            // Act
            controller.Create(newProduct);

            // Assert
            mockProductsService.Verify(x => x.Create(It.IsAny<Product>()));
        }

        [Fact]
        public void Edit_RedirectToHome_WhenProductNotExist()
        {
            // Arrange
            var id = 1;
            mockProductsService.Setup(x => x.Get(id)).Returns<Product>(null);

            // Act
            RedirectToActionResult redirectResult =
                   (RedirectToActionResult)controller.Edit(id);

            // Assert
            Assert.Equal(redirectResult.ActionName, "Index");
        }

        [Fact]
        public void Edit_EditProductCalled()
        {
            // Arrange
            var editProductVuewModel = new EditProductViewModel();

            // Act
            RedirectToActionResult redirectResult =
                   (RedirectToActionResult)controller.Edit(editProductVuewModel);

            // Assert
            mockProductsService.Verify(x => x.Update(It.IsAny<Product>()));
            Assert.Equal(redirectResult.ActionName, "Index");
        }

        private IList<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product() { ProductID = 1 }
            };
        }
    }
}
