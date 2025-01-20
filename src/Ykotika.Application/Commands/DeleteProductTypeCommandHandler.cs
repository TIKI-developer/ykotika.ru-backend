using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class DeleteProductTypeCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteProductTypeCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(DeleteProductTypeCommand request,
                   CancellationToken cancellationToken)
        {
            var productType = await
                _dbContext
                .ProductTypes
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(ProductType), request.Id);

            _dbContext.ProductTypes.Remove(productType);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
