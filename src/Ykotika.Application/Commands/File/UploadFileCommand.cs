using MediatR;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands.File
{
    public class UploadFileCommand : IRequest<FileDetails>
    {
        public required FileData FileData { get; set; }
        public string? RelativePath { get; set; }
    }
}
