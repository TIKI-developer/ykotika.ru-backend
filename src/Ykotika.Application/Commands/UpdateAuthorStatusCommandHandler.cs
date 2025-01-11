using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateAuthorStatusCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateAuthorStatusCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateAuthorStatusCommand request, CancellationToken cancellationToken)
        {
            var author = await
                _dbContext
                .Authors
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Author), request.Id);

            author.Status = request.NewStatus;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
