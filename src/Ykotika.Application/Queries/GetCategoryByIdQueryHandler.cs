using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetCategoryByIdQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper),
        IRequestHandler<GetCategoryByIdQuery, CategoryDetails>
    {
        public async Task<CategoryDetails> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext
                .Categories
                .Include(e => e.User)
                .Include(e => e.Image)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Category), request.Id);

            return _mapper.Map<CategoryDetails>(category);
        }
    }
}
