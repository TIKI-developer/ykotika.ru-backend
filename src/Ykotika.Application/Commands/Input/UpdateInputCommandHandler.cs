using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Input
{
    public class UpdateInputCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<UpdateInputCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateInputCommand request, CancellationToken cancellationToken)
        {
            var input = await
                _dbContext
                .FormInputs
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Input), request.Id);

            input.IsRequired = request.IsRequired ?? input.IsRequired;
            input.Label = request.Label ?? input.Label;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
