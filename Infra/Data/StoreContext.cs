using Core.Entities;
using Infra.Config;
using Microsoft.EntityFrameworkCore;


namespace Infra.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
     
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }

        public DbSet<Product> Products { get; set; }


    }
}
