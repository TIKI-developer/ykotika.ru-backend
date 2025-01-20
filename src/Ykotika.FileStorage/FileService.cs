using Ykotika.Application.Interfaces;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.FileStorage
{
    public class FileService() : IFileService
    {
        public string BaseStaticFolder
        {
            get
            {
                var path = Path.Combine(_baseFolder, "static");
                if (!File.Exists(path))
                {
                    Directory.CreateDirectory(path!);
                }
                return path;
            }
        }
        private readonly string _baseFolder = Path.Combine(AppContext.BaseDirectory, "uploads");

        public async Task<Domain.Entities.File> Upload(FileData data, string relativePath = "static", bool needUniqueName = true)
        {
            Guid id = Guid.NewGuid();
            string uniqueName;
            if (needUniqueName)
            {
                uniqueName = id + Path.GetExtension(data.Path);
            }
            else
            {
                uniqueName = data.Path;
            }

            string fullPath = Path.Combine(_baseFolder, relativePath, uniqueName);
            string directory = Path.GetDirectoryName(fullPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await stream.WriteAsync(data.Content);
            }

            return new Domain.Entities.File
            {
                Path = Path.Combine(relativePath, uniqueName).Replace("\\", "/"),
                Timestamps = new Timestamps()
            };
        }
        public async Task<FileData> Download(Domain.Entities.File file)
        {
            string filePath = Path.Combine(_baseFolder, file.Path);

            if (!System.IO.File.Exists(filePath))
            {
                throw new Exception("Файл не найден");
            }

            byte[] fileContent = await System.IO.File.ReadAllBytesAsync(filePath);

            return new FileData
            {
                Path = file.Path.Replace("\\", "/"),
                Content = fileContent
            };
        }
        public bool Delete(Domain.Entities.File file)
        {
            var filePath = Path.Combine(_baseFolder, file.Path);

            if (!File.Exists(filePath))
            {
                throw new Exception("Файл не найден");
            }

            try
            {
                File.Delete(filePath);
                return true;
            }
            catch (IOException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
