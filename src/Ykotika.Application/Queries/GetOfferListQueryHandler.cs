using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetOfferListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetOfferListQuery, BaseList<OfferItem>>
    {
        public async Task<BaseList<OfferItem>>
            Handle(GetOfferListQuery request,
                   CancellationToken cancellationToken)
        {
            var query =
                _dbContext
                .Offers
                .Where(e => !request.Filter.IsPublished.HasValue || e.IsPublished == request.Filter.IsPublished)
                .AsQueryable();

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems =
                query
                .AsNoTracking()
                .Include(e => e.Timestamps)
                .ProjectTo<OfferItem>(_mapper.ConfigurationProvider);

            return await BaseList<OfferItem>.CreateAsync(queryItems);
        }
    }
}
