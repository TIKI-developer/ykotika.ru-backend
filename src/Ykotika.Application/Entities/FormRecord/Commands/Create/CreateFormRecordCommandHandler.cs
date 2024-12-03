using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.FormRecord.Commands.Create
{
    public class CreateFormRecordCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<CreateFormRecordCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateFormRecordCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(e => e.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.FormId, cancellationToken)
                ?? throw new NotFoundException(nameof(FormModel), request.FormId);

            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(UserModel), request.UserId);

            var formRecord = new FormRecordModel
            {
                Id = Guid.NewGuid(),
                Form = form,
                User = user,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            foreach (var inputRecordRequest in request.InputRecords)
            {
                var formInput = await
                    _dbContext
                    .FormInputs
                    .FirstOrDefaultAsync(e => e.Id == inputRecordRequest.FormInputId, cancellationToken)
                    ?? throw new NotFoundException(nameof(FormInputModel), inputRecordRequest.FormInputId);

                var inputRecord = new FormInputRecordModel
                {
                    Id = Guid.NewGuid(),
                    FormInput = formInput,
                    SubmittedFormData = formRecord,
                    Value = inputRecordRequest.Value
                };

                formRecord.InputRecords.Add(inputRecord);

                await _dbContext.FormInputRecords.AddAsync(inputRecord, cancellationToken);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return formRecord.Id;
        }
    }
}
