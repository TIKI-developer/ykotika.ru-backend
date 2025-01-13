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
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetCategoryListQuery, PagedList<CategoryItem>>
    {
        public async Task<PagedList<CategoryItem>>
            Handle(GetCategoryListQuery request,
                   CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Categories
                .AsQueryable()
                .Where(e => !request.Filter.IsPublished.HasValue || e.IsPublished == request.Filter.IsPublished);

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query
                .AsNoTracking()
                .Include(e => e.Image)
                .ProjectTo<CategoryItem>(_mapper.ConfigurationProvider);

            return await PagedList<CategoryItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
