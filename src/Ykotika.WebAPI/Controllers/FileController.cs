using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.ValueObjects;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("files")]
    public class FileController : BaseController
    {
        [Authorize(Roles = $"{Roles.AUTHOR_ROLE}, {Roles.MODERATOR_ROLE}, {Roles.DIRECTOR_ROLE}, {Roles.ADMIN_ROLE}")]
        [HttpPost("upload")]
        public async Task<ActionResult<FileDetails>> Upload([FromForm] UploadFileDto dto)
        {
            var command = new UploadFileCommand { FileData = ConvertToFileData(dto.File), RelativePath = dto.RelativePath };
            var vm = await Mediator.Send(command);

            return Ok(vm);
        }
        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpDelete("{id}/delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteFileCommand { Id = id };
            await Mediator.Send(command);

            return Ok();
        }
        [Authorize(Roles = $"{Roles.MODERATOR_ROLE}, {Roles.DIRECTOR_ROLE}, {Roles.ADMIN_ROLE}")]
        [HttpGet("{id}/download")]
        public async Task<ActionResult> Download(Guid id)
        {
            var command = new DownloadFileCommand { Id = id };
            var file = await Mediator.Send(command);
            file.ContentType = GetContentType(file.Name);


            return Ok(File(file.Content, file.ContentType, file.Name));
        }

        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<FileList>> Get()
        {
            var query = new GetFileListQuery();
            var vm = await Mediator.Send(query);

            return Ok(vm);
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
        private static string GetContentType(string fileName)
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
