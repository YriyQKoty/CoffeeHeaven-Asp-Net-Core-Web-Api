using System;
using Library.Core.Concrete.Models;
using Library.Core.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<OriginCountry> Countries { get; set; }
        
        
        
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CoffeeConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}