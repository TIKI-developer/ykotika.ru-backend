using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Persistence.EntityTypeConfigurations;

namespace Ykotika.Persistence
{
    public class YkotikaDbContext(DbContextOptions<YkotikaDbContext> options) : DbContext(options), IYkotikaDbContext
    {
        public DbSet<Entity> Entities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OutsourceShop> OutsourceShops { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<Form> Forms { get; set; }
        public DbSet<Input> FormInputs { get; set; }
        public DbSet<FormRecord> FormRecords { get; set; }
        public DbSet<InputRecord> FormInputRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new FormConfiguration());
            modelBuilder.ApplyConfiguration(new FormInputConfiguration());
            modelBuilder.ApplyConfiguration(new FormRecordConfiguration());
            modelBuilder.ApplyConfiguration(new FormInputRecordConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
