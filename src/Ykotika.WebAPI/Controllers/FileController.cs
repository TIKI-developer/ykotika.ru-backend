using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;
using Ykotika.Application.Queries;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.ValueObjects;
using Ykotika.WebAPI.Constants;
using Ykotika.WebAPI.ModelBinders;
using Ykotika.WebAPI.Models;

namespace Ykotika.WebAPI.Controllers
{
    [Route("files")]
    public class FileController
        (IMapper mapper)
        : BaseController
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = $"{Roles.VERIFIED_ROLE}")]
        [HttpPost("upload")]
        public async Task<ActionResult<FileDetails>>
            Upload([FromForm] UploadFileDto dto)
        {
            var command = new UploadFileCommand
            {
                FileData = ConvertToFileData(dto.File),
                RelativePath = dto.Path
            };

            var vm = await Mediator.Send(command);

            return Ok(vm);
        }

        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpDelete("delete/{path}")]
        public async Task<ActionResult> Delete(string path)
        {
            var command = new DeleteFileCommand
            {
                Path = Uri.UnescapeDataString(path)
            };
            await Mediator.Send(command);

            return Ok();
        }

        [Authorize(Roles = $"{Roles.ADMIN_ROLE}, ${Roles.DIRECTOR_ROLE}, ${Roles.MODERATOR_ROLE}")]
        [HttpGet("download/{path}")]
        public async Task<ActionResult> Download(string path)
        {
            var command = new DownloadFileCommand
            {
                Path = Uri.UnescapeDataString(path)
            };
            var file = await Mediator.Send(command);
            file.ContentType = GetContentType(file.Path);

            return Ok(File(file.Content, file.ContentType, file.Path));
        }

        [Authorize(Roles = $"{Roles.ADMIN_ROLE}")]
        [HttpGet]
        public async Task<ActionResult<PagedList<FileItem>>>
            Get([FromQuery] FileListQueryParams queryParams)
        {
            var query = _mapper.Map<GetFileListQuery>(queryParams);
            var vm = await Mediator.Send(query);

            return Ok(vm);
        }

        private static FileData ConvertToFileData(IFormFile file)
        {
            using var memoryStream = new MemoryStream();

            file.CopyTo(memoryStream);

            return new FileData
            {
                Path = file.FileName,
                ContentType = file.ContentType,
                Content = memoryStream.ToArray()
            };
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
    public class FileListQueryParams : IMapWith<GetFileListQuery>
    {
        [ModelBinder(BinderType = typeof(PaginationBinder))]
        public PaginationQueryParams Pagination { get; set; } = new();
    }
}
