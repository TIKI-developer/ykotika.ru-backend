using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<AuthorModel>
    {
        public void Configure(EntityTypeBuilder<AuthorModel> builder)
        {
            builder
                .HasKey(e => e.UserId);
            builder
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<AuthorModel>(e => e.UserId);
            builder
                .OwnsOne(e => e.Request, request =>
                {
                    request.WithOwner();
                });
        }
    }
}
