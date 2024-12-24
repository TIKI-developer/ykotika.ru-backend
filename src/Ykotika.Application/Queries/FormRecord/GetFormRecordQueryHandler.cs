using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Queries.FormRecord
{
    public class GetFormRecordQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<GetFormRecordQuery, FormRecordViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<FormRecordViewModel> Handle(GetFormRecordQuery request, CancellationToken cancellationToken)
        {
            var formRecord = await
                _dbContext
                .FormRecords
                .Include(e => e.InputRecords)
                .Include(e => e.Form)
                .ThenInclude(e => e.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.Id);

            return _mapper.Map<FormRecordViewModel>(formRecord);
        }
    }
}
