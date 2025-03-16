using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetFormListQuery, PagedList<FormItem>>
    {
        public async Task<PagedList<FormItem>>
            Handle(GetFormListQuery request,
                   CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Forms
                .AsQueryable()
                .Where(p => !request.Filter.IsPublished.HasValue || p.IsPublished == request.Filter.IsPublished.Value);

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query
                .Include(e => e.Inputs)
                .AsNoTracking()
                .ProjectTo<FormItem>(_mapper.ConfigurationProvider);

            return await PagedList<FormItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
