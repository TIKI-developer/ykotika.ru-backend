using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.DeleteInput
{
    public class DeleteInputCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<DeleteInputCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(DeleteInputCommand request, CancellationToken cancellationToken)
        {
            var input = await
                _dbContext
                .FormInputs
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormInputModel), request.Id);
            _dbContext.FormInputs.Remove(input);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
