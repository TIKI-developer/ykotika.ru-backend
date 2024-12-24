using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Category
{
    public class DeleteCategoryCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext
                .Categories
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Category), request.Id);

            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
