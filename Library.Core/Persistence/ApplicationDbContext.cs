using Library.Core.Concrete.Models;
using Library.Core.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<OriginCountry> Countries { get; set; }
        
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CoffeeConfiguration());

            modelBuilder.ApplyConfiguration(new ProviderConfiguration());

            modelBuilder.ApplyConfiguration(new OriginCountryConfiguration());
        }
    }
}