using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries
{
    public class GetFormRecordListQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<GetFormRecordListQuery, FormRecordList>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormRecordList> Handle(GetFormRecordListQuery request, CancellationToken cancellationToken)
        {
            var formRecords = await
                _dbContext
                .FormRecords
                .Include(e => e.Form)
                .ProjectTo<FormRecordItem>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);


            return new FormRecordList { FormRecords = formRecords };
        }
    }
}
