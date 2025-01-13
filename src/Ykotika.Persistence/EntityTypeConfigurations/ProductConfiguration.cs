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
                .OwnsMany(e => e.OutsourceShops, os =>
                {
                    os.WithOwner();
                });
            builder
                .HasMany(e => e.Categories)
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

            builder
                .HasOne(c => c.Source)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);
            builder
                .OwnsMany(e => e.Tags, tag =>
                {
                    tag.WithOwner();
                });
        }
    }
}
