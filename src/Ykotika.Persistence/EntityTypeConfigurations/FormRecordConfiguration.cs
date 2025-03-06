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
                .HasOne(e => e.Form)
                .WithMany(e => e.FormRecords);
            builder
                .OwnsMany(e => e.InputRecords, ir =>
                {
                    ir.WithOwner().HasForeignKey(e => e.FormRecordId);
                    ir.HasKey(e => new { e.Id, e.FormRecordId });
                });
        }
    }
}
