using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Form
{
    public class DeleteFormCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteFormCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(DeleteFormCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Form), request.Id);

            _dbContext.Forms.Remove(form);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
