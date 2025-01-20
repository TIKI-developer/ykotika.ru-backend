using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FileConfiguration : IEntityTypeConfiguration<Domain.Entities.File>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.File> builder)
        {
            builder
                .HasKey(e => e.Path);

            builder
                .Property(e => e.Path)
                .IsRequired();

            builder
                .HasIndex(e => e.Path)
                .IsUnique();

            builder
                .OwnsOne(e => e.Timestamps, t =>
                {
                    t.WithOwner();
                });
        }
    }
}
