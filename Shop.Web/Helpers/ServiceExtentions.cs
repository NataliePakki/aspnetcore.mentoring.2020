using System.Collections.Generic;
using System.Linq;
using Shop.Core.Models;
using Shop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Shop.Web.Helpers
{
    public static class ServiceExtentions
    {
        public static IEnumerable<ProductViewModel> ToViewModels(this IEnumerable<Product> products)
        {
            return products?.Select(pr => pr.ToViewModel());
        }

        public static ProductViewModel ToViewModel(this Product product)
        {
            if (product == null)
                return null;
            return new ProductViewModel()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit,
                CategoryName = product.Category?.CategoryName,
                SupplierName = product.Supplier?.CompanyName
            };
        }

        public static IEnumerable<CategoryViewModel> ToViewModels(this IEnumerable<Category> categories)
        {
            return categories?.Select(pr => pr.ToViewModel());
        }

        public static CategoryViewModel ToViewModel(this Category category)
        {
            if (category == null)
                return null;
            return new CategoryViewModel()
            {
                CategoryId = category.CategoryID,
                Name = category.CategoryName
            };
        }

        public static EditCategoryViewModel ToEditViewModel(this Category category)
        {
            if (category == null)
                return null;
            return new EditCategoryViewModel()
            {
                CategoryId = category.CategoryID,
                Name = category.CategoryName,
                Description = category.Description,
                FileContent = category.Picture,
                Result = string.Empty
            };
        }

        public static Category ToCategory(this EditCategoryViewModel viewModel)
        {
            if (viewModel == null)
                return null;
            return new Category()
            {
                CategoryID = viewModel.CategoryId,
                CategoryName = viewModel.Name,
                Picture = viewModel.FileContent,
                Description = viewModel.Description
            };
        }

        public static CreateProductViewModel ToCreateViewModel(this Product product)
        {
            if (product == null)
                return null;
            return new CreateProductViewModel()
            {
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit
            };
        }

        public static IList<SelectListItem> ToSelectList(this IEnumerable<Category> categories)
        {
            if (categories == null)
                return null;
            return categories.Select(x => x.ToSelectListItem()).ToList();
        }

        public static SelectListItem ToSelectListItem(this Category category)
        {
            if (category == null)
                return null;
            return new SelectListItem()
            {
                Value = category.CategoryID.ToString(),
                Text = category.CategoryName
            };
        }


        public static IList<SelectListItem> ToSelectList(this IEnumerable<Supplier> suppliers)
        {
            if (suppliers == null)
                return null;
            return suppliers.Select(x => x.ToSelectListItem()).ToList();
        }

        public static SelectListItem ToSelectListItem(this Supplier supplier)
        {
            if (supplier == null)
                return null;
            return new SelectListItem()
            {
                Value = supplier.SupplierID.ToString(),
                Text = supplier.CompanyName
            };
        }

        public static Product ToProduct(this CreateProductViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Product()
            {
                ProductName = viewModel.ProductName,
                QuantityPerUnit = viewModel.QuantityPerUnit,
                UnitPrice = viewModel.UnitPrice,
                UnitsInStock = viewModel.UnitsInStock,
                CategoryID = viewModel.CategoryID,
                SupplierID = viewModel.SupplierID
            };
        }

        public static EditProductViewModel ToEditViewModel(this Product product)
        {
            if (product == null)
                return null;
            return new EditProductViewModel()
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice,
                QuantityPerUnit = product.QuantityPerUnit
            };
        }

        public static Product ToProduct(this EditProductViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            return new Product()
            {
                ProductID = viewModel.ProductID,
                ProductName = viewModel.ProductName,
                QuantityPerUnit = viewModel.QuantityPerUnit,
                UnitPrice = viewModel.UnitPrice,
                UnitsInStock = viewModel.UnitsInStock,
                CategoryID = viewModel.CategoryID,
                SupplierID = viewModel.SupplierID
            };
        }
    }
}
