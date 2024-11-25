using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.File.Commands.Download
{
    public class DownloadCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService)
        :
        IRequestHandler<DownloadCommand, FileData>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IFileService _fileService = fileService;

        public async Task<FileData> Handle(DownloadCommand request, CancellationToken cancellationToken)
        {
            var file = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (file == null)
            {
                throw new NotFoundException(nameof(FileModel), request.Id);
            }

            var data = await _fileService.Download(file);

            return data;
        }
    }
}
