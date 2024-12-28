using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
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
                ?? throw new NotFoundException(nameof(Category), request.Id);

            category.Name = request.Name ?? category.Name;
            category.Description = request.Description ?? category.Description;

            var image = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Id == request.ImageFileId, cancellationToken);

            category.Image = image ?? category.Image;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
