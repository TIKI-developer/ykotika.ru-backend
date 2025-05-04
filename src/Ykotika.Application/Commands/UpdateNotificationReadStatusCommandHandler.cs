using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    internal class UpdateNotificationReadStatusCommandHandler 
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateNotificationReadStatusCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task 
            Handle(UpdateNotificationReadStatusCommand request, CancellationToken cancellationToken)
        {
            var notification = await _dbContext
                .Notifications
                .FirstOrDefaultAsync(e => e.Id == request.Id && e.User.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(Notification), request.Id);

            notification.IsRead = request.IsRead;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
