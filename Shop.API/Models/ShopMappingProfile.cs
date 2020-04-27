using AutoMapper;
using Shop.Core.Models;

namespace Shop.API.Models
{
    public class ShopMappingProfile: Profile
    {
        public ShopMappingProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<Category, CategoryModel>();
            CreateMap<Supplier, SupplierModel>();
        }
    }
}
