using Microsoft.EntityFrameworkCore;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Interfaces
{
    public interface IYkotikaDbContext
    {
        DbSet<Entity> Entities { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Moderator> Moderators { get; set; }
        DbSet<Admin> Admin { get; set; }
        DbSet<Director> Directors { get; set; }
        DbSet<Domain.Entities.File> Files { get; set; }
        DbSet<Form> Forms { get; set; }
        DbSet<Input> FormInputs { get; set; }
        DbSet<FormRecord> FormRecords { get; set; }
        DbSet<InputRecord> FormInputRecords { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
