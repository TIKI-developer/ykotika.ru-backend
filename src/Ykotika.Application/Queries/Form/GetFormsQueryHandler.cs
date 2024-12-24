using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.Form
{
    public class GetFormsQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<GetFormsQuery, FormListViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormListViewModel> Handle(GetFormsQuery request, CancellationToken cancellationToken)
        {
            var forms = await
                _dbContext
                .Forms
                .ProjectTo<FormLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new FormListViewModel { Forms = forms };
        }
    }
}
