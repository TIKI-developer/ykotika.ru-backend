using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands
{
    internal class UpdateAllNotificationsReadStatusCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateAllNotificationsReadStatusCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateAllNotificationsReadStatusCommand request, CancellationToken cancellationToken)
        {
            var notifications = await
                _dbContext
                .Notifications
                .Where(e => e.User.Id == request.UserId)
                .ToListAsync(cancellationToken);

            notifications.ForEach(e => e.IsRead = request.IsRead);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
