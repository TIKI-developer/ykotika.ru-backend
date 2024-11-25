using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<VerifyEmailCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(UserModel), request.UserId);
            }

            if (!user.Roles.Contains(UserRole.Guest))
            {
                return;
            }

            var customer = new CustomerModel
            {
                UserId = user.Id,
            };

            user.Roles.Remove(UserRole.Guest);
            user.Roles.Add(UserRole.Customer);
            user.MarkUpdated();

            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
