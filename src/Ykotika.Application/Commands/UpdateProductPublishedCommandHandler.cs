using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateProductPublishedCommandHandler 
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProductPublishedCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task 
            Handle(UpdateProductPublishedCommand request, 
                   CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), cancellationToken);

            product.IsPublished = request.IsPublished;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
