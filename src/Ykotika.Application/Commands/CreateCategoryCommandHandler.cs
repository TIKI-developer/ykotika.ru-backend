using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateCategoryCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var image = await
                _dbContext
                .Files
                .FirstOrDefaultAsync(e => e.Id == request.ImageFileId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.File), request.ImageFileId);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Image = image,
                Timestamps = new Timestamps(),
                IsPublished = false
            };

            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return category.Id;
        }
    }
}
