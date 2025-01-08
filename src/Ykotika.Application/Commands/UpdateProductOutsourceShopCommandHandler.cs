using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateProductOutsourceShopCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProductOutsourceShopCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateProductOutsourceShopCommand request, CancellationToken cancellationToken)
        {
            var outsourceShops = await
                _dbContext
                .OutsourceShops
                .Where(e => request.OutsourceShops.Contains(e.Id))
                .ToListAsync(cancellationToken);

            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            product.OutsourceShops = outsourceShops;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
