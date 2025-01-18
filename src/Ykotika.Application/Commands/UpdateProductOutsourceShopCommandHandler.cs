using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class UpdateProductOutsourceShopCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProductOutsourceShopCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(UpdateProductOutsourceShopCommand request,
                   CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            foreach (var shop in request.OutsourceShopInfo)
            {
                var outsourceShop = await
                    _dbContext
                    .OutsourceShops
                    .FirstOrDefaultAsync(e => e.Id == shop.OutsourceShopId, cancellationToken);

                if (outsourceShop == null)
                {
                    continue;
                }

                product.OutsourceShops.Add(new OutsourceShopProductInfo
                {
                    OutsourceShop = outsourceShop,
                    Link = shop.Link,
                });
            }


            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
