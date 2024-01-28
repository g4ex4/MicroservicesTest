using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProductApi.Entities;
using ProductApi.Infrastructure.EntityTypeConfiguration;

namespace ProductApi.Infrastructure
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductItem> Products { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new ProductItemConfiguration());
        }
    }
    public class ProductContextDesignFactory : IDesignTimeDbContextFactory<ProductContext>
    {
        public ProductContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ProductContext> optionsBuilder = new DbContextOptionsBuilder<ProductContext>()
                .UseSqlServer();

            return new ProductContext(optionsBuilder.Options);
        }
    }
}
