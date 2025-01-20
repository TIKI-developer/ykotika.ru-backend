using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Queries
{
    public class GetFormByIdRecordQueryHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        : BaseGetQueryHandler(dbContext, mapper),
        IRequestHandler<GetFormByIdRecordQuery, FormRecordDetails>
    {
        public async Task<FormRecordDetails>
            Handle(GetFormByIdRecordQuery request,
                   CancellationToken cancellationToken)
        {
            var formRecord = await
                _dbContext
                .FormRecords
                .Include(e => e.InputRecords)
                .Include(e => e.Form)
                .ThenInclude(e => e.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.Id);

            return _mapper.Map<FormRecordDetails>(formRecord);
        }
    }
}
