using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands
{
    public class DeleteFileCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService)
        :
        IRequestHandler<DeleteFileCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IFileService _fileService = fileService;

        public async Task Handle(DeleteFileCommand request, CancellationToken cancellationToken)
        {
            var file = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.Id);

            if (_fileService.Delete(file))
            {
                _dbContext.Files.Remove(file);

                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception(Messages.FILE_DELETE_ERROR);
            }
        }
    }
}
