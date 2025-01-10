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
        : IRequestHandler<GetAuthorListQuery, AuthorList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<AuthorList> Handle(GetAuthorListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Authors
                .Include(e => e.User)
                .Include(e => e.Socials)
                .Include(e => e.Request)
                .AsQueryable();

            query = query
                .Where(e => string.IsNullOrEmpty(request.Name) || e.User.Name == request.Name)
                .Where(e => string.IsNullOrEmpty(request.Surname) || e.User.Surname == request.Surname)
                .Where(e => string.IsNullOrEmpty(request.PhoneNumber) || e.User.PhoneNumber == request.PhoneNumber)
                .Where(e => string.IsNullOrEmpty(request.Email) || e.User.Email == request.Email)
                .Where(e => !request.ContactSocial.HasValue || e.Request.WhichSocial == request.ContactSocial)
                .Where(e => !request.Status.HasValue || e.Status == request.Status)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                query = request.IsDescending
                    ? query.OrderByDescending(c => EF.Property<object>(c, request.SortBy))
                    : query.OrderBy(c => EF.Property<object>(c, request.SortBy));
            }

            var authors = await
                query
                .ProjectTo<AuthorItem>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new AuthorList { Authors = authors };
        }
    }
}
