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
        : IRequestHandler<GetOfferListQuery, OfferList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<OfferList> Handle(GetOfferListQuery request, CancellationToken cancellationToken)
        {
            var offers = await
                _dbContext
                .Offers
                .Include(e => e.Timestamps)
                .ProjectTo<OfferItem>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new OfferList { Offers = offers };
        }
    }
}
