using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateOutsourceShopCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateOutsourceShopCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateOutsourceShopCommand request, CancellationToken cancellationToken)
        {
            var outsourceShop = await
                _dbContext
                .OutsourceShops
                .Include(e => e.Timestamps)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(OutsourceShop), request.Id);


            outsourceShop.Name = request.Name ?? outsourceShop.Name;
            outsourceShop.Link = request.Link ?? outsourceShop.Link;

            if (request.ImagePath != null)
            {
                var logo = await
                    _dbContext
                    .Files
                    .FirstOrDefaultAsync(e => e.Path == request.ImagePath, cancellationToken)
                    ?? throw new NotFoundException(nameof(Domain.Entities.File), request.ImagePath);
                outsourceShop.Image = logo ?? outsourceShop.Image;
            }
            outsourceShop.Timestamps.MarkUpdated();

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
