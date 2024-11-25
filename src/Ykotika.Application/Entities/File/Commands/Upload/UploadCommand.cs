using MediatR;

namespace Ykotika.Application.Entities.File.Commands.Upload
{
    public class UploadCommand : IRequest<FileViewModel>
    {
        public required FileData FileData { get; set; }
        public string? RelativePath { get; set; }
    }
}
