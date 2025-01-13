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
        : BaseGetListQueryHandler(dbContext, mapper),
        IRequestHandler<GetFormRecordListQuery, BaseList<FormRecordItem>>
    {
        public async Task<BaseList<FormRecordItem>>
            Handle(GetFormRecordListQuery request,
                   CancellationToken cancellationToken)
        {
            var query =
                _dbContext
                .FormRecords
                .AsQueryable();

            var queryItems =
                query
                .Include(e => e.Form)
                .ProjectTo<FormRecordItem>(_mapper.ConfigurationProvider);

            return await BaseList<FormRecordItem>.CreateAsync(queryItems);
        }
    }
}