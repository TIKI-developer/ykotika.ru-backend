using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateFormRecordCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<CreateFormRecordCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid>
            Handle(CreateFormRecordCommand request,
                   CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(e => e.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.FormId, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.FormId);

            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            var inputRecords = new List<FormRecord.InputRecord>();

            foreach (var inputRecordRequest in request.InputRecords)
            {
                var inputRecord = new FormRecord.InputRecord
                {
                    Id = inputRecordRequest.Id,
                    Value = inputRecordRequest.Value,
                };
                inputRecords.Add(inputRecord);
            }

            var formRecord = new FormRecord
            {
                Id = Guid.NewGuid(),
                Form = form,
                User = user,
                InputRecords = inputRecords,
                Timestamps = new Timestamps()
            };

            await _dbContext.FormRecords.AddAsync(formRecord, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return formRecord.Id;
        }
    }
}
