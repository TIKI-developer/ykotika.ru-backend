using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Product
{
    public class UpdateProductCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProductCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Product), request.Id);

            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.OutsourceShops = request.OutsourceShops ?? product.OutsourceShops;
            product.Images = request.Images ?? product.Images;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
