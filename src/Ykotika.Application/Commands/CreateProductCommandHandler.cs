using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.Models;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommandHandler
        (IYkotikaDbContext dbContext,
        IArticleGenerator articleGenerator)
        : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IArticleGenerator _articleGenerator = articleGenerator;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productType = await
                _dbContext
                .ProductTypes
                .FirstOrDefaultAsync(e => e.Id == request.ProductTypeId, cancellationToken)
                ?? throw new NotFoundException(nameof(ProductType), request.ProductTypeId);

            var outSourceShops = await
                _dbContext
                .OutsourceShops
                .ToListAsync(cancellationToken);

            var source = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Id == request.SourceId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.SourceId);

            var formRecord = await
                _dbContext
                .FormRecords
                .Include(e => e.InputRecords)
                .ThenInclude(e => e.FormInput)
                .FirstOrDefaultAsync(e => e.Id == request.FormRecordId, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.FormRecordId);

            var fileOrderMapping = request.Images.ToDictionary(
                item => item.FileId,
                item => item.OrderIndex);

            var fileIds = fileOrderMapping.Keys.ToList();

            var files = await _dbContext.Files
                .Where(file => fileIds.Contains(file.Id))
                .ToListAsync();

            var imageListItems = files
                .Select(file => new ImageListItem
                {
                    File = file,
                    OrderIndex = fileOrderMapping[file.Id]
                })
                .OrderBy(item => item.OrderIndex)
                .ToList();

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Source = source,
                Tags = request.Tags,
                Status = ProductStatus.New,
                Article = _articleGenerator.Generate(productType.ArticlePattern, formRecord),
                Name = request.Name,
                Description = request.Description,
                Images = imageListItems,
                Timestamps = new Timestamps(),
                FormRecord = formRecord,
                IsPublished = false,
                OutsourceShops = outSourceShops,
                ProductType = productType,
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
