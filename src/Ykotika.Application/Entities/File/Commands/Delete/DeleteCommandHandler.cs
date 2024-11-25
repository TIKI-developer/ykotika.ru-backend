using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.File.Commands.Delete
{
    public class DeleteCommandHandler
        (IYkotikaDbContext dbContext,
        IFileService fileService)
        :
        IRequestHandler<DeleteCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IFileService _fileService = fileService;

        public async Task Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var file = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

            if (file == null)
            {
                throw new NotFoundException(nameof(FileModel), request.Id);
            }
            if (_fileService.Delete(file))
            {
                _dbContext.Files.Remove(file);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            else
            {
                throw new Exception("File delete error");
            }
        }
    }
}
