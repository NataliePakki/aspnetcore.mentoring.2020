
using AspNetCoreMentoring.Models;
using Microsoft.EntityFrameworkCore;


namespace AspNetCoreMentoring.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options) {}

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Product>()
            //  .HasOne(a => a.Category)
            //  .WithOne(b => b.Product)
            //  .HasForeignKey<Product>(b => b.CategoryID);
            //modelBuilder.Entity<Product>()
            //    .HasOne(a => a.Category).WithOne(b => b.Product)
            //    .HasForeignKey<Category>(e => e.ProductID);
            //        modelBuilder.Entity<Author>().ToTable("Authors");
            //        modelBuilder.Entity<AuthorBiography>().ToTable("Authors");

            base.OnModelCreating(modelBuilder);
        }
    }
}
