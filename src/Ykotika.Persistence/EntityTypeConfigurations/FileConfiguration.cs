using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FileConfiguration : IEntityTypeConfiguration<FileModel>
    {
        public void Configure(EntityTypeBuilder<FileModel> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(e => e.RelativePath)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .HasIndex(e => new { e.Name, e.RelativePath })
                .IsUnique();
        }
    }
}
