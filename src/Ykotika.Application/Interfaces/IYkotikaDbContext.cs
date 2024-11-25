using Microsoft.EntityFrameworkCore;
using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IYkotikaDbContext
    {
        DbSet<UserModel> Users { get; set; }
        DbSet<FileModel> Files { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
