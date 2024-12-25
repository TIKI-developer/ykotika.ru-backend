using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormInputConfiguration
        : IEntityTypeConfiguration<Input>
    {
        public void Configure(EntityTypeBuilder<Input> builder)
        {
            builder
                .HasOne(e => e.Form)
                .WithMany(e => e.Inputs);
            builder
                .Property(e => e.Label)
                .HasMaxLength(255);
            builder
                .Property(e => e.IsRequired)
                .HasDefaultValue(false);
        }
    }
}
