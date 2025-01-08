using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetCategoryListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetCategoryListQuery, CategoryList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<CategoryList>
            Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var query = 
                _dbContext
                .Categories
                .Include(e => e.Image)
                .AsQueryable();


            if (request.IsPublished.HasValue)
            {
                query = query.Where(e => e.IsPublished == request.IsPublished);
            }

            var categories = await
                query
                .ProjectTo<CategoryItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CategoryList { Categories = categories };
        }
    }
}
