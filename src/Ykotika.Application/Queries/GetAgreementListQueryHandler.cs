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
            var agreements = await
                _dbContext
                .Agreements
                .Include(e => e.Author)
                .ProjectTo<AgreementItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AgreementList { Agreements = agreements };
        }
    }
}
