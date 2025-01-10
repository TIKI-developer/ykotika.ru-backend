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
            var author = await
                _dbContext
                .Authors
                .FirstOrDefaultAsync(e => e.UserId == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(Author), request.UserId);

            if (!author.Status.Equals(AuthorStatus.Confirmed))
            {
                throw new Exception("Author is not active!");
            }

            var currentOffer = await
                _dbContext
                .Offers
                .OrderByDescending(e => e.Timestamps.UpdatedAt)
                .FirstOrDefaultAsync(cancellationToken)
                ?? throw new NotFoundException(nameof(Offer), "current");

            var agreement = await
                _dbContext
                .Agreements
                .FirstOrDefaultAsync(e => e.Offer.Id == currentOffer.Id && e.Author.UserId == request.UserId)
                ?? throw new Exception("You need accept current offer!");

            var productType = await
                _dbContext
                .ProductTypes
                .FirstOrDefaultAsync(e => e.Id == request.ProductTypeId, cancellationToken)
                ?? throw new NotFoundException(nameof(ProductType), request.ProductTypeId);

            var outSourceShops = await
                _dbContext
                .OutsourceShops
                .ToListAsync(cancellationToken);

            var outsourceShopsInfo = new List<OutsourceShopProductInfo>();

            foreach (var shop in outSourceShops)
            {
                outsourceShopsInfo.Add(new OutsourceShopProductInfo
                {
                    OutsourceShop = shop,
                    Link = shop.Link,
                });
            }

            var source = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Path == request.SourcePath, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.SourcePath);
            
            List<Category> categories = [];

            if (request.CategoryIds != null)
            {
                categories = await
                    _dbContext
                    .Categories
                    .Where(e => request.CategoryIds.Contains(e.Id))
                    .ToListAsync(cancellationToken);
            }

            var formRecord = await
                _dbContext
                .FormRecords
                .Include(e => e.InputRecords)
                .Include(e => e.Form)
                .FirstOrDefaultAsync(e => e.Id == request.FormRecordId, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.FormRecordId);

            var fileOrderMapping = request.Images.ToDictionary(
                item => item.ImagePath,
                item => item.OrderIndex);

            var fileIds = fileOrderMapping.Keys.ToList();

            var files = await 
                _dbContext
                .Files
                .Where(file => fileIds.Contains(file.Path))
                .ToListAsync(cancellationToken);

            var imageListItems = files
                .Select(file => new ImageListItem
                {
                    Image = file,
                    OrderIndex = fileOrderMapping[file.Path]
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
                Author = author,
                Images = imageListItems,
                Timestamps = new Timestamps(),
                FormRecord = formRecord,
                IsPublished = false,
                Categories = categories,
                OutsourceShops = outsourceShopsInfo,
                ProductType = productType,
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
