using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Shop.Core.Data;
using Shop.Core.Models;
using Shop.Core.Services;

namespace Shop.Core.Services
{
    public class ProductService: IProductService
    {
        private ApplicationContext _context;

        public ProductService(ApplicationContext context, IConfiguration configuration)
        {
            _context = context;
        }

        public Product Create(Product data)
        {
            _context.Products.Add(data);
            _context.SaveChanges();
            return data;
        }

        public Product Get(int id)
        {
            return _context.Products.FirstOrDefault(x => x.ProductID == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products
                .Include(product => product.Category)
                .Include(product => product.Supplier);
        }

        public Product Update(Product data)
        {
            _context.Update(data);
            _context.SaveChanges();
            return data;
        }
    }
}
