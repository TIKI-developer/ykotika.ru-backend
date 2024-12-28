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
            var categories = await
                _dbContext
                .Categories
                .ProjectTo<CategoryItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CategoryList { Categories = categories };
        }
    }
}
