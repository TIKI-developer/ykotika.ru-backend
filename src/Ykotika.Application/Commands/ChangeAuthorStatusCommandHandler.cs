using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class ChangeAuthorStatusCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<ChangeAuthorStatusCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(ChangeAuthorStatusCommand request, CancellationToken cancellationToken)
        {
            var author = await
                _dbContext
                .Authors
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Author), request.Id);

            author.Status = request.NewStatus;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
