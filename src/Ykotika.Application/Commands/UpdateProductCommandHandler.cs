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

        public async Task
            Handle(UpdateProductCommand request,
                   CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.InputRecords)
                .Include(e => e.Categories)
                .Include(e => e.Images)
                .Include(e => e.Tags)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);


            if (product.Status is not ProductStatus.New)
            {
                throw new Exception("Сейчас товар изменить нельзя!");
            }

            List<Category>? categories = null;

            if (request.CategoryIds != null)
            {
                categories = await
                    _dbContext
                    .Categories
                    .Where(e => request.CategoryIds.Contains(e.Id))
                    .ToListAsync(cancellationToken) ?? null;

                product.Categories = categories;
            }

            if (request.FormRecord != null)
            {
                foreach (var inputRecordRequest in request.FormRecord.InputRecords)
                {
                    product.FormRecord.InputRecords
                        .FirstOrDefault
                        (e => e.Id == inputRecordRequest.Id)!
                        .Value = inputRecordRequest.Value;
                }
            }

            product.Name = request.Name ?? product.Name;
            product.Description = request.Description ?? product.Description;
            product.Tags = request.Tags ?? product.Tags;

            if (request.SourcePath != null)
            {
                var source = await
                    _dbContext
                    .Files
                    .FirstOrDefaultAsync(e => e.Path == request.SourcePath, cancellationToken)
                    ?? throw new NotFoundException(nameof(Domain.Entities.File), request.SourcePath);

                product.Source = source;
            }

            if (request.Images != null)
            {
                var images = await
                    _dbContext
                    .Files
                    .Where(e => request.Images.Select(i => i.ImagePath).Contains(e.Path))
                    .ToListAsync(cancellationToken);
                var productImages = request.Images
                    .Select(dto => new ImageListItem
                    {
                        OrderIndex = dto.OrderIndex,
                        Image = images.FirstOrDefault(e => e.Path == dto.ImagePath)
                    })
                    .OrderBy(image => image.OrderIndex)
                    .ToList();

                product.Images = productImages ?? product.Images;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
