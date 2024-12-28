using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.Agreement
{
    public class GetAgreementByUserQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : IRequestHandler<GetAgreementByUserQuery, AgreementList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<AgreementList> Handle(GetAgreementByUserQuery request, CancellationToken cancellationToken)
        {
            var agreements = await
                _dbContext
                .Agreements
                .Include(e => e.Author)
                .Where(e => e.Author.Id == request.AuthorId)
                .ProjectTo<AgreementItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AgreementList { Agreements = agreements };
        }
    }
}
