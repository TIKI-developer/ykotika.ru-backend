using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerModel>
    {
        public void Configure(EntityTypeBuilder<CustomerModel> builder)
        {
            builder
                .HasKey(e => e.UserId);
            builder
                .HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<CustomerModel>(e => e.UserId);
        }
    }
}
