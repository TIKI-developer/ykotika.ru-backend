using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class OutsourceShopConfiguration : IEntityTypeConfiguration<OutsourceShop>
    {
        public void Configure(EntityTypeBuilder<OutsourceShop> builder)
        {
            builder
                .HasOne(e => e.Logo);
            builder
                .HasMany(e => e.Products)
                .WithMany(e => e.OutsourceShops);
        }
    }
}
