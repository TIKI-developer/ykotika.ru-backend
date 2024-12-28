using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class DeleteFormRecordCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<DeleteFormRecordCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(DeleteFormRecordCommand request, CancellationToken cancellationToken)
        {
            var formRecord = await
                _dbContext
                .FormRecords
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormRecord), request.Id);

            _dbContext.FormRecords.Remove(formRecord);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
