using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormRecordConfiguration
        : IEntityTypeConfiguration<FormRecord>
    {
        public void Configure(EntityTypeBuilder<FormRecord> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .HasOne(e => e.Form)
                .WithMany(e => e.SubmittedForms);
            builder
                .HasMany(e => e.InputRecords)
                .WithOne(e => e.SubmittedFormData);
        }
    }
}
