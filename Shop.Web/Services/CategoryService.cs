using System.Collections.Generic;
using System.Linq;
using AspNetCoreMentoring.Data;
using AspNetCoreMentoring.Models;

namespace AspNetCoreMentoring.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationContext _context;

        public CategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public Category Create(Category data)
        {
            _context.Categories.Add(data);
            _context.SaveChanges();
            return data;
        }

        public Category Get(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.CategoryID == id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories;
        }

        public Category Update(Category data)
        {
            _context.Update(data);
            _context.SaveChanges();
            return data;
        }
    }
}
