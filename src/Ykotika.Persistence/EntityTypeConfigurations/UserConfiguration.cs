using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasIndex(e => e.Email)
                .IsUnique();
            builder
                .Property(e => e.Email)
                .HasMaxLength(256);
            builder
                .HasOne(e => e.Image);

            builder
                .HasMany(e => e.Agreements)
                .WithOne(e => e.User);
        }
    }
}
