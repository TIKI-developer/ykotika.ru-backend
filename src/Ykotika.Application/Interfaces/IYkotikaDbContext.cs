using Microsoft.EntityFrameworkCore;
using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IYkotikaDbContext
    {
        DbSet<UserModel> Users { get; set; }
        DbSet<AuthorModel> Authors { get; set; }
        DbSet<CustomerModel> Customers { get; set; }
        DbSet<FileModel> Files { get; set; }
        DbSet<FormModel> Forms { get; set; }
        DbSet<FormInputModel> FormInputs { get; set; }
        DbSet<FormRecordModel> FormRecords { get; set; }
        DbSet<FormInputRecordModel> FormInputRecords { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
