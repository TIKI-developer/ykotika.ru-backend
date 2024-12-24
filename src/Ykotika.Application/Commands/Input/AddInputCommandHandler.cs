using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Input
{
    public class AddInputCommandHandler(IYkotikaDbContext dbContext) : IRequestHandler<AddInputCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(AddInputCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .FirstOrDefaultAsync(e => e.Id == request.FormId, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.FormId);

            var input = new Domain.Entities.Input
            {
                Id = Guid.NewGuid(),
                OrderIndex = request.OrderIndex,
                Form = form,
                Label = request.Label,
                Type = request.Type,
                IsRequired = request.IsRequired
            };

            await _dbContext.FormInputs.AddAsync(input, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return input.Id;
        }
    }
}
