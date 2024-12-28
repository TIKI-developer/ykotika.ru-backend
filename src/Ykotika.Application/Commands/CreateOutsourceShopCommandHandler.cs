using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateOutsourceShopCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateOutsourceShopCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateOutsourceShopCommand request, CancellationToken cancellationToken)
        {
            var logoFile = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Id == request.LogoFileId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.LogoFileId);

            var outsourceShop = new OutsourceShop
            {
                Id = request.LogoFileId,
                Timestamps = new Timestamps(),
                Name = request.Name,
                Link = request.Link,
                Logo = logoFile,
            };

            await _dbContext.OutsourceShops.AddAsync(outsourceShop, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return outsourceShop.Id;
        }
    }
}
