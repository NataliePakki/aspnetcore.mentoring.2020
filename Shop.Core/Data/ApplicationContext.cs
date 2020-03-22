using Shop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Shop.Core.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options) {}

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
