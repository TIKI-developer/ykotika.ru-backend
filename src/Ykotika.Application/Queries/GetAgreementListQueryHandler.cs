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
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetAgreementListQuery, PagedList<AgreementItem>>
    {
        public async Task<PagedList<AgreementItem>>
            Handle(GetAgreementListQuery request,
                   CancellationToken cancellationToken)
        {
            var query = _dbContext
                .Agreements
                .Include(e => e.Offer)
                .Include(e => e.User)
                .Include(e => e.Timestamps)
                .AsQueryable()
                .Where(e =>
                    (!request.Filter.UserId.HasValue || e.User.Id == request.Filter.UserId) &&
                    (!request.Filter.OfferId.HasValue || e.Offer.Id == request.Filter.OfferId));

            query = Sort(query, request.Sorting.SortBy, request.Sorting.IsDescending);

            var queryItems = query.ProjectTo<AgreementItem>(_mapper.ConfigurationProvider);

            return await PagedList<AgreementItem>.CreateAsync(queryItems, request.Pagination.Page, request.Pagination.PageSize);
        }
    }
}
