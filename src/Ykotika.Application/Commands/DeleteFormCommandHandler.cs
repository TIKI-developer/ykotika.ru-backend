using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class DeleteFormCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteFormCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(DeleteFormCommand request,
                   CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.Id);

            _dbContext.Forms.Remove(form);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
