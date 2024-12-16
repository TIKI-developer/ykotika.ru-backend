using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.FormRecord.Commands.Update
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
            foreach (var inputRecordRequest in request.InputRecords)
            {
                var inputRecord = await
                    _dbContext
                    .FormInputRecords
                    .FirstOrDefaultAsync(e => e.Id == inputRecordRequest.Id)
                    ?? throw new NotFoundException(nameof(FormInputModel), inputRecordRequest.Id);
                inputRecord.Value = inputRecordRequest.Value;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
