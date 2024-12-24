using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Category
{
    public class CreateCategoryCommandHandler(IYkotikaDbContext dbContext) : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .FirstOrDefaultAsync(e => e.Id == request.FormId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Form), request.FormId);

            var category = new Domain.Entities.Category
            {
                Id = Guid.NewGuid(),
                Name = form.Name,
                Form = form,
                Timestamps = new Domain.ValueObjects.Timestamps()
            };

            return category.Id;
        }
    }
}
