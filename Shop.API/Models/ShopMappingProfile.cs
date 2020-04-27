using AutoMapper;
using Shop.Core.Models;

namespace Shop.API.Models
{
    public class ShopMappingProfile: Profile
    {
        public ShopMappingProfile()
        {
            CreateMap<Product, ProductModel>()
                .ReverseMap();
            CreateMap<Category, CategoryModel>()
                .ReverseMap();
            CreateMap<Supplier, SupplierModel>()
                .ReverseMap();
        }
    }
}
