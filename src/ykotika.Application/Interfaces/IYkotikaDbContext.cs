using Microsoft.EntityFrameworkCore;
using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IYkotikaDbContext
    {
        DbSet<UserModel> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
