using CURDOperation.Models;
using Microsoft.EntityFrameworkCore;

namespace CURDOperation
{
    public class CURDOperationDbContext : DbContext
    {
        public CURDOperationDbContext(DbContextOptions<CURDOperationDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);
        }
    }
}
