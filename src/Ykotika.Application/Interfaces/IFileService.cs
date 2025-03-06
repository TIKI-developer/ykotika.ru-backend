using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Interfaces
{
    public interface IFileService
    {
        Task<Domain.Entities.File> Upload(FileData data, string relativePath = "static", bool needUniqueName = true);
        Task<FileData> Download(Domain.Entities.File file);
        bool Delete(Domain.Entities.File file);
        Task<FileData> Duplicate(Domain.Entities.File file);
        string BaseStaticFolder { get; }
    }
}
