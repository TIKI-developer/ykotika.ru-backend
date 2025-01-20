using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class DeleteOutsourceShopCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteOutsourceShopCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task
            Handle(DeleteOutsourceShopCommand request,
                   CancellationToken cancellationToken)
        {
            var outsourceShop = await
                _dbContext
                .OutsourceShops
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(OutsourceShop), request.Id);

            _dbContext.OutsourceShops.Remove(outsourceShop);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
