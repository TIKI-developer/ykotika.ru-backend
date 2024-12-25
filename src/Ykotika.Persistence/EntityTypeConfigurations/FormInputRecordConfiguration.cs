using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormInputRecordConfiguration
        : IEntityTypeConfiguration<InputRecord>
    {
        public void Configure(EntityTypeBuilder<InputRecord> builder)
        {
            builder
                .HasOne(e => e.SubmittedFormData)
                .WithMany(e => e.InputRecords);
            builder
                .HasOne(e => e.FormInput)
                .WithMany(e => e.SubmittedFormFieldsData);
        }
    }
}
