using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Record
{
    public class CreateRecordCommandHandler
        (IYkotikaDbContext dbContext)
        : 
        IRequestHandler<CreateRecordCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateRecordCommand request, CancellationToken cancellationToken)
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
                    Value = inputRecordRequest.Value
                };

            }

            var formRecord = new FormRecordModel
            {
                Id = Guid.NewGuid(),
                Form = form,
                User = user,

                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };


            return Guid.Empty;
        }
    }
}
