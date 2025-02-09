using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateProductStatusCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProductStatusCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateProductStatusCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            switch (product.Status)
            {
                case ProductStatus.Edit:
                    if (request.NewStatus is not ProductStatus.PendingModeration) 
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.PendingModeration:
                    if (request.NewStatus is not ProductStatus.Edit ||
                        request.NewStatus is not ProductStatus.Moderating)
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.Moderating:
                    if (request.NewStatus is not ProductStatus.Incorrect ||
                        request.NewStatus is not ProductStatus.Done)
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.Done:
                    if (request.NewStatus is not ProductStatus.Selling ||
                        request.NewStatus is not ProductStatus.Incorrect)
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.Selling:
                    if (request.NewStatus is not ProductStatus.Selling ||
                        request.NewStatus is not ProductStatus.Incorrect ||
                        request.NewStatus is not ProductStatus.NotSelling)
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.NotSelling:
                    if (request.NewStatus is not ProductStatus.Selling)
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.Incorrect:
                    if (request.NewStatus is not ProductStatus.Fixed ||
                        request.NewStatus is not ProductStatus.Moderating)
                        throw new UnavailableOperation();

                    break;
                case ProductStatus.Fixed:
                    if (request.NewStatus is not ProductStatus.Moderating)
                        throw new UnavailableOperation();

                    break;
            }

            product.Status = request.NewStatus;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
