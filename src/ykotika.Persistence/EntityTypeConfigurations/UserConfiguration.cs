using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .HasIndex(e => e.Email)
                .IsUnique();
            builder
                .Property(e => e.Email)
                .HasMaxLength(256);
        }
    }
}
