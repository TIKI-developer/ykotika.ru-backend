using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormsQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<GetFormsQuery, FormList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormList> Handle(GetFormsQuery request, CancellationToken cancellationToken)
        {
            var forms = await
                _dbContext
                .Forms
                .ProjectTo<FormItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new FormList { Forms = forms };
        }
    }
}
