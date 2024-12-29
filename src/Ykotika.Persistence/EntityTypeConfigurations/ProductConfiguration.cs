using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasMany(e => e.OutsourceShops)
                .WithMany(e => e.Products);
            builder
                .HasOne(e => e.FormRecord);
            builder
                .HasOne(e => e.ProductType)
                .WithMany(e => e.Products);
            builder
                .OwnsMany(e => e.Images, image =>
                {
                    image.WithOwner();
                });
        }
    }
}
