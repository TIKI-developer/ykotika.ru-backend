using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateProductCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext
                .Categories
                .FirstOrDefaultAsync(e => e.Id == request.CategoryId, cancellationToken)
                ?? throw new NotFoundException(nameof(Category), request.CategoryId);

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
                Name = formRecord.InputRecords.FirstOrDefault(e => e.FormInput.Label == "Название")!.Value,
                Description = formRecord.InputRecords.FirstOrDefault(e => e.FormInput.Label == "Описание")!.Value,
                Timestamps = new Timestamps(),
                FormRecord = formRecord
            };

            return product.Id;
        }
    }
}
