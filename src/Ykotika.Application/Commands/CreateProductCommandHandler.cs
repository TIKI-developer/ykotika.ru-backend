using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
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

            var formRecord = await
                _dbContext
                .FormRecords
                .Include(e => e.InputRecords)
                .ThenInclude(e => e.FormInput)
                .FirstOrDefaultAsync(e => e.Id == request.FormRecordId, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.FormRecordId);

            var product = new Product
            {
                Id = Guid.NewGuid(),
                Article = _articleGenerator.Generate(productType.ArticlePattern, formRecord),
                Name = request.Name ?? formRecord.InputRecords.FirstOrDefault(e => e.FormInput.Label == "Название").Value,
                Description = request.Description ?? formRecord.InputRecords.FirstOrDefault(e => e.FormInput.Label == "Описание").Value,
                Timestamps = new Timestamps(),
                FormRecord = formRecord,
                IsPublished = false,
                ProductType = productType,
            };

            await _dbContext.Products.AddAsync(product, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
