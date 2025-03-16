using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateProductsBySpreadsheetCommandHandler
        (IYkotikaDbContext dbContext,
        ISpreadsheetWorker spreadsheetService,
        IFileService fileService)
        : IRequestHandler<CreateProductsBySpreadsheetCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly ISpreadsheetWorker _spreadsheetService = spreadsheetService;
        private readonly IFileService _fileService = fileService;

        public async Task
            Handle(CreateProductsBySpreadsheetCommand request, CancellationToken cancellationToken)
        {
            var spreadsheetFile = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Path == request.SpreadsheetFilePath, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.SpreadsheetFilePath);

            var filesZip = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Path == request.ZipFilePath, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.ZipFilePath);

            var spreadsheetFileData = await _fileService.Download(spreadsheetFile);
            var fileZipData = await _fileService.Download(filesZip);

            var spreadsheetProducts = await _spreadsheetService.GenerateProductRequests(spreadsheetFileData, fileZipData);

            foreach (var spreadsheetProduct in spreadsheetProducts)
            {
                var user = await
                    _dbContext
                    .Users
                    .FirstOrDefaultAsync(e => e.Email == spreadsheetProduct.UserEmail, cancellationToken);
                if (user == null)
                {
                    Console.WriteLine($"Не найден пользователь с почтовым ящиком: {spreadsheetProduct.UserEmail}");
                    continue;
                }

                var tags = new List<Tag>();
                foreach (var tag in spreadsheetProduct.Tags)
                {
                    tags.Add(new Tag { Value = tag });
                }

                var outsourceShops = new List<OutsourceShopProductInfo>();
                if (spreadsheetProduct.OutsourceShops != null)
                {
                    foreach (var outsourceShopFromSpreadsheet in spreadsheetProduct.OutsourceShops)
                    {
                        var outsourceShop = await
                            _dbContext
                            .OutsourceShops
                            .FirstOrDefaultAsync(e => e.Name == outsourceShopFromSpreadsheet.Name, cancellationToken);

                        if (outsourceShop == null)
                        {
                            Console.WriteLine($"Не найден внешний магазин с наименованием: {outsourceShopFromSpreadsheet.Name}");
                            continue;
                        }

                        outsourceShops.Add(new OutsourceShopProductInfo
                        {
                            OutsourceShop = outsourceShop,
                            Link = outsourceShopFromSpreadsheet.Link,
                        });
                    }
                }

                var productType = await
                    _dbContext
                    .ProductTypes
                    .Include(e => e.Form)
                    .FirstOrDefaultAsync(e => e.Name == spreadsheetProduct.ProductTypeName, cancellationToken);
                if (productType == null)
                {
                    Console.WriteLine($"Не найден тип товара с наименованием: {spreadsheetProduct.ProductTypeName}");
                    continue;
                }

                var inputRecords = new List<FormRecord.InputRecord>();
                foreach (var inputRecord in spreadsheetProduct.FormRecord.InputRecords)
                {
                    var id = productType.Form.Inputs.FirstOrDefault(e => e.ExtraAttributes.Label == inputRecord.Name)?.Id;

                    if (id == null)
                    {
                        Console.WriteLine($"Не найдено поле с наименованием: {inputRecord.Name}");
                        continue;
                    }

                    inputRecords.Add(new FormRecord.InputRecord
                    {
                        Id = id,
                        Value = inputRecord.Value
                    });
                }

                var formRecord = new FormRecord
                {
                    Id = Guid.NewGuid(),
                    Form = productType.Form,
                    Timestamps = new Timestamps(),
                    InputRecords = inputRecords,
                    User = user,
                };
                await _dbContext.FormRecords.AddAsync(formRecord, cancellationToken);

                var categories = await
                    _dbContext
                    .Categories
                    .Where(e => spreadsheetProduct.CategoryNames.Contains(e.Name))
                    .ToListAsync(cancellationToken);

                await _dbContext.Files.AddAsync(spreadsheetProduct.Files.Source, cancellationToken);

                foreach (var imageFile in spreadsheetProduct.Files.Images)
                {
                    await _dbContext.Files.AddAsync(imageFile.Image, cancellationToken);
                }

                var newProduct = new Product
                {
                    Id = Guid.NewGuid(),
                    Article = spreadsheetProduct.Article,
                    Name = spreadsheetProduct.Name,
                    Description = spreadsheetProduct.Description,
                    Timestamps = new Timestamps(),
                    IsPublished = spreadsheetProduct.IsPublished,
                    IsAdult = spreadsheetProduct.IsAdult,
                    Status = spreadsheetProduct.Status,
                    Tags = tags,
                    Categories = categories,
                    Images = spreadsheetProduct.Files.Images,
                    OutsourceShops = outsourceShops,
                    FormRecord = formRecord,
                    ProductType = productType,
                    Source = spreadsheetProduct.Files.Source,
                    User = user,
                };
                await _dbContext.Products.AddAsync(newProduct, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
