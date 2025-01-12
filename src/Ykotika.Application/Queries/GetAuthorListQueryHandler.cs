using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAuthorListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper) 
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetAuthorListQuery, PagedList<AuthorItem>>
    {
        public async Task<PagedList<AuthorItem>>
            Handle(GetAuthorListQuery request,
                   CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Authors
                .Where(e => string.IsNullOrEmpty(request.Filter.Name) || e.User.Name == request.Filter.Name)
                .Where(e => string.IsNullOrEmpty(request.Filter.Surname) || e.User.Surname == request.Filter.Surname)
                //.Where(e => !request.Filter.ContactSocial.HasValue || e.Request.WhichSocial == request.Filter.ContactSocial)
                //.Where(e => !request.Filter.Status.HasValue || e.Status == request.Filter.Status)
                .AsQueryable();

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query
                .Include(e => e.User)
                .Include(e => e.Socials)
                .Include(e => e.Request)
                .AsNoTracking()
                .ProjectTo<AuthorItem>(_mapper.ConfigurationProvider);

            return await PagedList<AuthorItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
