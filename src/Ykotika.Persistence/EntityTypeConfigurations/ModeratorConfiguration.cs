using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ykotika.Domain.Entities;

namespace Ykotika.Persistence.EntityTypeConfigurations
{
    public class ModeratorConfiguration : IEntityTypeConfiguration<Moderator>
    {
        public void Configure(EntityTypeBuilder<Moderator> builder) { }
    }
}
