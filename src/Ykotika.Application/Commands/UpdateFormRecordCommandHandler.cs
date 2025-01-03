using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateFormRecordCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<UpdateFormRecordCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task Handle(UpdateFormRecordCommand request, CancellationToken cancellationToken)
        {
            var formRecord = await
                _dbContext
                .FormRecords
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.Id);

            foreach (var inputRecordRequest in formRecord.InputRecords)
            {
                formRecord
                    .InputRecords
                    .FirstOrDefault
                    (e => e.Id == inputRecordRequest.Id)!
                    .Value = inputRecordRequest.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
