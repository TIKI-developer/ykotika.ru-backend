using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetProductListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetProductListQuery, PagedList<ProductItem>>
    {
        public async Task<PagedList<ProductItem>>
            Handle(GetProductListQuery request,
                   CancellationToken cancellationToken)
        {
            var query = _dbContext.Products
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                query = query.Where(p =>
                    p.Name.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(request.SearchTerm.ToLower()) ||
                    p.Tags.Any(tag => tag.Value.ToLower().Contains(request.SearchTerm.ToLower())));
            }

            query = query.Where(p =>
                (!request.Filter.IsPublished.HasValue || p.IsPublished == request.Filter.IsPublished.Value) &&
                (!request.Filter.UserId.HasValue || p.User.Id == request.Filter.UserId.Value) &&
                (!request.Filter.ProductTypeId.HasValue || p.ProductType.Id == request.Filter.ProductTypeId.Value) &&
                (!request.Filter.CategoryId.HasValue || p.Categories.Any(e => e.Id == request.Filter.CategoryId.Value)));


            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query
                .Include(e => e.User)
                .Include(e => e.ProductType)
                .Include(e => e.Images)
                .ThenInclude(e => e.Image)
                .Include(e => e.FormRecord)
                .Include(e => e.OutsourceShops)
                .ProjectTo<ProductItem>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<ProductItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }

    }
}
