using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetAgreementListByAuthorQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetAgreementListByAuthorQuery, AgreementList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<AgreementList> Handle(GetAgreementListByAuthorQuery request, CancellationToken cancellationToken)
        {
            var agreements = await
                _dbContext
                .Authors
                .Where(e => e.Id == request.Id)
                .ProjectTo<AgreementItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AgreementList { Agreements = agreements };
        }
    }
}
