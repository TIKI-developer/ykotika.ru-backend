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
        : IRequestHandler<GetFormListQuery, FormList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormList> Handle(GetFormListQuery request, CancellationToken cancellationToken)
        {
            var query =
                _dbContext
                .Forms
                .Include(e => e.Inputs)
                .AsQueryable();

            if (request.IsPublished.HasValue)
            {
                query = query.Where(p => p.IsPublished == request.IsPublished.Value);
            }

            var forms = await query
                .ProjectTo<FormItem>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new FormList { Forms = forms };
        }
    }
}
