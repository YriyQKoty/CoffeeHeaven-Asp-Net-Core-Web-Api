using Library.Core.Concrete.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Core.Persistence.EntityConfigurations
{
    public class CoffeeConfiguration : IEntityTypeConfiguration<Coffee>
    {
        public void Configure(EntityTypeBuilder<Coffee> builder)
        {
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("Brand");

            builder.HasKey(b => b.Id);

            builder
                .Property(b => b.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();
            
                
        }
    }
}