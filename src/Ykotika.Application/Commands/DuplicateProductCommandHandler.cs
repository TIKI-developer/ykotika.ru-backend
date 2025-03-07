using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class DuplicateProductCommandHandler
        (IYkotikaDbContext dbContext,
        IArticleGenerator articleGenerator,
        IFileService fileService)
        : IRequestHandler<DuplicateProductCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IArticleGenerator _articleGenerator = articleGenerator;
        private readonly IFileService _fileService = fileService;

        public async Task
            Handle(DuplicateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await
                _dbContext
                .Products
                .Include(e => e.Source)
                .Include(e => e.ProductType)
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.InputRecords)
                .Include(e => e.FormRecord)
                .ThenInclude(e => e.Form)
                .Include(e => e.Images)
                .ThenInclude(e => e.Image)
                .Include(e => e.OutsourceShops)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Product), request.Id);

            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            var newImages = new List<ImageListItem>();

            foreach (var image in product.Images)
            {
                Console.WriteLine(image.Image.Path);
                var newFileData = await _fileService.Duplicate(image.Image);

                var newImageFile = new Domain.Entities.File
                {
                    Path = newFileData.Path,
                    Timestamps = new Timestamps()
                };
                var newImage = new ImageListItem
                {
                    Image = newImageFile,
                    OrderIndex = image.OrderIndex,
                };
                Console.WriteLine(newImage.Image.Path);
                newImages.Add(newImage);
                await _dbContext.Files.AddAsync(newImageFile, cancellationToken);
            }

            var inputRecords = new List<FormRecord.InputRecord>();

            foreach (var inputRecord in product.FormRecord.InputRecords)
            {
                var newInputRecord = new FormRecord.InputRecord
                {
                    Id = inputRecord.Id,
                    Value = inputRecord.Value,
                };
                inputRecords.Add(newInputRecord);
            }

            var formRecord = new FormRecord
            {
                Id = Guid.NewGuid(),
                Timestamps = new Timestamps(),
                Form = product.FormRecord.Form,
                InputRecords = inputRecords,
                User = user,
            };

            await _dbContext.FormRecords.AddAsync(formRecord, cancellationToken);

            var newTags = new List<Tag>();

            foreach (var tag in product.Tags)
            {
                newTags.Add(new Tag
                {
                    Value = tag.Value,
                });
            }

            var newProduct = new Product
            {
                Id = Guid.NewGuid(),
                Timestamps = new Timestamps(),
                Article = _articleGenerator.Generate(product.ProductType.ArticlePattern, product),
                Name = product.Name,
                Description = product.Description,
                IsPublished = false,
                IsAdult = product.IsAdult,
                Status = ProductStatus.Edit,
                Tags = newTags,
                OutsourceShops = product.OutsourceShops,
                ProductType = product.ProductType,
                User = user,
                Images = newImages,
                FormRecord = formRecord,
            };

            if (product.Source != null)
            {
                Console.WriteLine(product.Source.Path);
                var newFileData = await _fileService.Duplicate(product.Source);

                var newSource = new Domain.Entities.File
                {
                    Path = newFileData.Path,
                    Timestamps = new Timestamps()
                };
                newProduct.Source = newSource;
                Console.WriteLine(newSource.Path);
                await _dbContext.Files.AddAsync(newSource, cancellationToken);
            }


            await _dbContext.Products.AddAsync(newProduct, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
