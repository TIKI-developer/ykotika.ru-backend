using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormInputRecordConfiguration
        : IEntityTypeConfiguration<FormInputRecordModel>
    {
        public void Configure(EntityTypeBuilder<FormInputRecordModel> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .HasOne(e => e.SubmittedFormData)
                .WithMany(e => e.FieldsData);
            builder
                .HasOne(e => e.FormField)
                .WithMany(e => e.SubmittedFormFieldsData);
        }
    }
}
