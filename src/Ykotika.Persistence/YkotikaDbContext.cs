using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;
using Ykotika.Persistence.EntityTypeConfigurations;

namespace Ykotika.Persistence
{
    public class YkotikaDbContext(DbContextOptions<YkotikaDbContext> options) : DbContext(options), IYkotikaDbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<AuthorModel> Authors { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<FormModel> Forms { get; set; }
        public DbSet<FormInputModel> FormInputs { get; set; }
        public DbSet<FormRecordModel> FormRecords { get; set; }
        public DbSet<FormInputRecordModel> FormInputRecords { get; set; }

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
