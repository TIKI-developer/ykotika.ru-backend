using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormInputConfiguration
        : IEntityTypeConfiguration<FormInputModel>
    {
        public void Configure(EntityTypeBuilder<FormInputModel> builder)
        {
            builder
                .HasKey(e => e.Id);
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
