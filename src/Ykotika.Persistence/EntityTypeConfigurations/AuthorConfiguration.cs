using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder
                .OwnsOne(e => e.Request, ar =>
                {
                    ar.WithOwner();
                    ar.OwnsOne(e => e.Timestamps);
                });

            builder
                .OwnsMany(e => e.Socials, s =>
                {
                    s.WithOwner();
                });

            builder
                .HasMany(e => e.Agreements)
                .WithOne(e => e.Author);

            builder
                .HasOne(e => e.User);
        }
    }
}
