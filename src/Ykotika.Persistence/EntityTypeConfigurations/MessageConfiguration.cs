using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasMany(e => e.Attachments)
                .WithMany();
            builder
                .HasOne(e => e.Chat)
                .WithMany(e => e.Messages)
                .HasForeignKey(e => e.ChatId);
            builder
                .HasOne(e => e.Sender)
                .WithMany();
            builder
                .Property(e => e.Text);
        }
    }
}
