using Microsoft.EntityFrameworkCore;
using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IYkotikaDbContext
    {
        DbSet<UserModel> Users { get; set; }
        DbSet<AuthorModel> Authors { get; set; }
        DbSet<CustomerModel> Customers { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
