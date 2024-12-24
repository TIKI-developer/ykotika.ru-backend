using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Category
{
    public class UpdateCategoryCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext
                .Categories
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);

            category.Name = request.Name ?? category.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
