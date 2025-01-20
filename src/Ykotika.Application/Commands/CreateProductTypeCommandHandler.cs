using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateProductTypeCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateProductTypeCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid>
            Handle(CreateProductTypeCommand request,
                   CancellationToken cancellationToken)
        {
            var author = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.AuthorId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.AuthorId);

            var form = await
                _dbContext
                .Forms
                .FirstOrDefaultAsync(e => e.Id == request.FormId, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.FormId);

            var productType = new ProductType
            {
                Id = Guid.NewGuid(),
                ArticlePattern = request.ArticlePattern,
                Name = request.Name,
                Form = form,
                Timestamps = new Timestamps(),
                IsPublished = request.IsPublished,
                User = author
            };

            await _dbContext.ProductTypes.AddAsync(productType, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return productType.Id;
        }
    }
}
