using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Core.Persistence.EntityConfigurations
{
    public class OriginCountryConfiguration :IEntityTypeConfiguration<OriginCountry>
    
    {
        public void Configure(EntityTypeBuilder<OriginCountry> builder)
        {
            builder.Property(oc => oc.Id)
                .IsRequired();

            builder.Property(oc => oc.Name)
                .HasMaxLength(255)
                .IsRequired();
            
           builder
                .HasMany(c => c.Providers)
                .WithOne(p => p.OriginCountry)
                .HasForeignKey(p => p.OriginCountryId);
        }
    }
}