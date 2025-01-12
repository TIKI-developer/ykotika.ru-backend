using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateProductTypeCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProductTypeCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(UpdateProductTypeCommand request,
                   CancellationToken cancellationToken)
        {
            var productType = await
                _dbContext
                .ProductTypes
                .Include(e => e.Timestamps)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(ProductType), request.Id);

            productType.Name = request.Name ?? productType.Name;
            productType.ArticlePattern = request.ArticlePattern ?? productType.ArticlePattern;
            productType.IsPublished = request.IsPublished ?? productType.IsPublished;
            productType.Timestamps.MarkUpdated();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
