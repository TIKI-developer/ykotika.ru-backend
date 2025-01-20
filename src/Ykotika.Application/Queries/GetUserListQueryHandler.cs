using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetUserListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetUserListQuery, PagedList<UserItem>>
    {
        public async Task<PagedList<UserItem>>
            Handle(GetUserListQuery request,
                   CancellationToken cancellationToken)
        {
            var query =
                _dbContext
                .Users
                .AsQueryable();

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query
                .Include(e => e.Image)
                .ProjectTo<UserItem>(_mapper.ConfigurationProvider);

            return await PagedList<UserItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}