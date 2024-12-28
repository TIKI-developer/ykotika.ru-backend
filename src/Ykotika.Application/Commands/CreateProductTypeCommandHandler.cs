using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateProductTypeCommandHandler(IYkotikaDbContext dbContext) : IRequestHandler<CreateProductTypeCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProductTypeCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .FirstOrDefaultAsync(e => e.Id == request.FormId, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.FormId);

            var category = new ProductType
            {
                Id = Guid.NewGuid(),
                Name = form.Name,
                Form = form,
                Timestamps = new Timestamps(),
                IsPublished = false
            };

            return category.Id;
        }
    }
}
