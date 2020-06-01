using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Core.Persistence.EntityConfigurations
{
    public class ProviderConfiguration :IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.Property(p => p.Id)
                .IsRequired();
            
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(511);
            
            builder
                .HasMany(p => p.Coffees)
                .WithOne(c => c.Provider)
                .HasForeignKey(c => c.ProviderId);

        }
    }
}