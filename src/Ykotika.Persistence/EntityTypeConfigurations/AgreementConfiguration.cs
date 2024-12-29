using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class AgreementConfiguration : IEntityTypeConfiguration<Agreement>
    {
        public void Configure(EntityTypeBuilder<Agreement> builder)
        {
            builder
                .HasOne(e => e.Offer)
                .WithMany(e => e.Agreements);

            builder
                .HasOne(e => e.Author)
                .WithMany(e => e.Agreements);
        }
    }
}
