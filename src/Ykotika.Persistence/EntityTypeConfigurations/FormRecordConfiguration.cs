using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormRecordConfiguration
        : IEntityTypeConfiguration<FormRecordModel>
    {
        public void Configure(EntityTypeBuilder<FormRecordModel> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .HasOne(e => e.Form)
                .WithMany(e => e.SubmittedForms);
            builder
                .HasOne(e => e.User)
                .WithMany(e => e.SubmittedForms);
            builder
                .HasMany(e => e.InputRecords)
                .WithOne(e => e.SubmittedFormData);
            builder
                .Property(e => e.CreatedAt)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("timezone('UTC', now())");
            builder
                .Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("timezone('UTC', now())");
        }
    }
}
