using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class FormConfiguration
        : IEntityTypeConfiguration<FormModel>
    {
        public void Configure(EntityTypeBuilder<FormModel> builder)
        {
            builder
                .HasKey(e => e.Id);
            builder
                .Property(e => e.Name)
                .HasMaxLength(100);
            builder
                .HasMany(e => e.Inputs)
                .WithOne(e => e.Form);
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
