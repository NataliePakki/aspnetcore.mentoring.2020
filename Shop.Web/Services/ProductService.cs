using System.Collections.Generic;
using System.Linq;
using AspNetCoreMentoring.Data;
using AspNetCoreMentoring.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AspNetCoreMentoring.Services
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
