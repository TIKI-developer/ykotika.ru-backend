using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder
                .HasMany(e => e.Messages)
                .WithOne(e => e.Chat);

            builder
                .HasMany(e => e.Members)
                .WithMany();

            builder
                .Property(e => e.Name);
        }
    }
}
