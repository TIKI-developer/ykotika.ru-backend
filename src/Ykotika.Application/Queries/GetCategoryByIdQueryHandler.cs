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
        : IRequestHandler<GetCategoryByIdQuery, CategoryDetails>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryDetails> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await
                _dbContext
                .Categories
                .Include(e => e.Image)
                .FirstOrDefaultAsync(e => e.Id == request.Id)
                ?? throw new NotFoundException(nameof(Category), request.Id);

            return _mapper.Map<CategoryDetails>(category);
        }
    }
}
