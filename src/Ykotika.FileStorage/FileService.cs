using Ykotika.Application.Interfaces;
using Ykotika.Domain.ValueObjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<FileData> Duplicate(Domain.Entities.File file)
        {
            string sourcePath = Path.Combine(_baseFolder, file.Path);

            if (!File.Exists(sourcePath))
            {
                throw new Exception("Файл не найден");
            }

            string directory = Path.GetDirectoryName(sourcePath)!;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(sourcePath);
            string copyFileName = $"{fileName}{extension}";
            string copyPath = Path.Combine(directory, copyFileName);

            byte[] content = await File.ReadAllBytesAsync(sourcePath);

            await File.WriteAllBytesAsync(copyPath, content);

            return new FileData
            {
                Path = copyPath.Replace(_baseFolder, "").Replace("\\", "/").TrimStart('/'),
                Content = content
            };
        }

    }
}
