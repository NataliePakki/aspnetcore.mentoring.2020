using System;
using AutoMapper;
using Shop.Core.Models;
using Shop.Web.ViewModels;

namespace Shop.Web.Models
{
    public class ViewMappingProfile : Profile
    {
        public ViewMappingProfile()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(prv => prv.CategoryName, opt => opt.MapFrom(pr => pr.Category != null ? pr.Category.CategoryName : null))
                .ForMember(prv => prv.SupplierName, opt => opt.MapFrom(pr => pr.Supplier != null ? pr.Supplier.CompanyName : null));


            CreateMap<Product, CreateProductViewModel>()
                .ReverseMap();

            CreateMap<Product, EditProductViewModel>()
                .ReverseMap();

            CreateMap<Category, CategoryViewModel>()
                .ReverseMap();

            CreateMap<Category, EditCategoryViewModel>()
                 .ForMember(prv => prv.FileContent, opt => opt.MapFrom(pr => pr.Picture))
                 .ReverseMap();
            ;
        }
    }
}
