using Microsoft.AspNetCore.Mvc;
using Ykotika.Application;
using Ykotika.Application.Entities.File.Commands.Delete;
using Ykotika.Application.Entities.File.Commands.Download;
using Ykotika.Application.Entities.File.Commands.Upload;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("files")]
    public class FileController : BaseController
    {
        [HttpPost("upload")]
        public async Task<ActionResult<FileViewModel>> Upload([FromForm] UploadFileDto dto)
        {
            var command = new UploadCommand { FileData = ConvertToFileData(dto.File), RelativePath = dto.RelativePath };
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [HttpGet("download/{id}")]
        public async Task<ActionResult> Download(Guid id)
        {
            var command = new DownloadCommand { Id = id };
            var file = await Mediator.Send(command);
            file.ContentType = GetContentType(file.Name);


            return Ok(File(file.Content, file.ContentType, file.Name));
        }
        private static FileData ConvertToFileData(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return new FileData
                {
                    Name = file.FileName,
                    ContentType = file.ContentType,
                    Content = memoryStream.ToArray()
                };
            }
        }
        private string GetContentType(string fileName)
        {
            var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
