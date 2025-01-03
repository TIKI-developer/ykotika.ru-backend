using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormConfiguration
        : IEntityTypeConfiguration<Form>
    {
        public void Configure(EntityTypeBuilder<Form> builder)
        {
            builder
                .Property(e => e.Name)
                .HasMaxLength(100);
            builder
                .OwnsMany(e => e.Inputs, i =>
                {
                    i.WithOwner();
                    i.Property(e => e.Label)
                     .HasMaxLength(255);
                    i.Property(e => e.IsRequired)
                     .HasDefaultValue(false);
                });
            builder
                .HasMany(e => e.FormRecords)
                .WithOne(e => e.Form);
        }
    }
}
