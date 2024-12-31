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

        DbSet<OutsourceShop> OutsourceShops { get; set; }
        DbSet<Agreement> Agreements { get; set; }
        DbSet<Offer> Offers { get; set; }

        DbSet<Form> Forms { get; set; }
        DbSet<FormRecord> FormRecords { get; set; }

        DbSet<ProductType> ProductTypes { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set; }

        DbSet<Domain.Entities.File> Files { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
