using Ykotika.Domain;

namespace Ykotika.Application.Interfaces
{
    public interface IFileService
    {
        Task<FileModel> Upload(FileData data, string relativePath = "static");
        Task<FileData> Download(FileModel file);
        bool Delete(FileModel file);
        string BaseStaticFolder { get; }
    }
}
