using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands.File
{
    public class DownloadFileCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService)
        :
        IRequestHandler<DownloadFileCommand, FileData>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IFileService _fileService = fileService;

        public async Task<FileData> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
        {
            var file = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.Id); ;

            var data = await _fileService.Download(file);

            return data;
        }
    }
}
