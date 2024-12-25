using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class EntityConfiguration : IEntityTypeConfiguration<Entity>
    {
        public void Configure(EntityTypeBuilder<Entity> builder)
        {
            builder.UseTptMappingStrategy();

            builder
                .HasKey(e => e.Id);
            builder
                .HasIndex(e => e.Id)
                .IsUnique();

            builder
                .OwnsOne(e => e.Timestamps, t =>
                {
                    t.WithOwner();
                });
        }
    }
}
