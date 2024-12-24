using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.FormRecord
{
    public class GetFormRecordListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<GetFormRecordListQuery, FormRecordListViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormRecordListViewModel> Handle(GetFormRecordListQuery request, CancellationToken cancellationToken)
        {
            var formRecords = await
                _dbContext
                .FormRecords
                .Include(e => e.Form)
                .ProjectTo<FormRecordLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new FormRecordListViewModel { FormRecords = formRecords };
        }
    }
}
