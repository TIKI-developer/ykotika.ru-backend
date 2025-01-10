using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetAgreementListQuery, AgreementList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<AgreementList> Handle(GetAgreementListQuery request, CancellationToken cancellationToken)
        {
            var query =
                _dbContext
                .Agreements
                .Include(e => e.Timestamps)
                .Include(e => e.Offer)
                .Include(e => e.Author)
                .AsQueryable();

            if (request.AuthorId.HasValue)
            {
                query = query.Where(e => e.Author.UserId == request.AuthorId);
            }
            if (request.OfferId.HasValue)
            {
                query = query.Where(e => e.Offer.Id == request.OfferId);
            }

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                query = request.IsDescending
                    ? query.OrderByDescending(c => EF.Property<object>(c, request.SortBy))
                    : query.OrderBy(c => EF.Property<object>(c, request.SortBy));
            }

            var agreements = await
                query
                .ProjectTo<AgreementItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AgreementList { Agreements = agreements };
        }
    }
}
