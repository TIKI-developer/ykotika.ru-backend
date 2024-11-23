using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;
using Ykotika.Persistence.EntityTypeConfigurations;

namespace Ykotika.Persistence
{
    public class YkotikaDbContext(DbContextOptions<YkotikaDbContext> options) : DbContext(options), IYkotikaDbContext
    {
        public DbSet<UserModel> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
