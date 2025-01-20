using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class DeleteProductCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteProductCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(DeleteProductCommand request,
                   CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
