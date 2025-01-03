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
            var authors = await
                _dbContext
                .Authors
                .Include(e => e.User)
                .Include(e => e.Request)
                .Include(e => e.Timestamps)
                .AsNoTracking()
                .ProjectTo<AuthorItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AuthorList { Authors = authors };
        }
    }
}
