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
                .OwnsOne(e => e.Request, request =>
                {
                    request.WithOwner();
                });
        }
    }
}
