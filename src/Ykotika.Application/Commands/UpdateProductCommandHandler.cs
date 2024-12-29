using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class UpdateProductCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<UpdateProductCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);


            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;

            if (request.Images != null)
            {
                var images = await
                    _dbContext
                    .Files
                    .Where(e => request.Images.Select(i => i.FileId).Contains(e.Id))
                    .ToListAsync(cancellationToken);
                var productImages = request.Images
                    .Select(dto => new ImageListItem
                    {
                        OrderIndex = dto.OrderIndex,
                        File = images.FirstOrDefault(e => e.Id == dto.FileId)
                    })
                    .OrderBy(image => image.OrderIndex)
                    .ToList();

                product.Images = productImages ?? product.Images;
            }

            if (request.OutsourceShops != null)
            {
                var outsourceShops = await
                    _dbContext
                    .OutsourceShops
                    .Where(e => request.OutsourceShops.Contains(e.Id))
                    .ToListAsync(cancellationToken);

                product.OutsourceShops = outsourceShops ?? product.OutsourceShops;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
