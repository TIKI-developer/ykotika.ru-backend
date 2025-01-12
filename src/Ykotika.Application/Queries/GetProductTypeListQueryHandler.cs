using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductTypeListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetListQueryHandler(dbContext, mapper), 
        IRequestHandler<GetProductTypeListQuery, PagedList<ProductTypeItem>>
    {
        public async Task<PagedList<ProductTypeItem>>
            Handle(GetProductTypeListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.ProductTypes
                .AsQueryable()
                .Where(pt => !request.Filter.IsPublished.HasValue || pt.IsPublished == request.Filter.IsPublished.Value);

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query.ProjectTo<ProductTypeItem>(_mapper.ConfigurationProvider);

            return await PagedList<ProductTypeItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
