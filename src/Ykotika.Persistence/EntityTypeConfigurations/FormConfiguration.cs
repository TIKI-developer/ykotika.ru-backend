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
                .HasMany(e => e.Inputs)
                .WithOne(e => e.Form);
        }
    }
}
