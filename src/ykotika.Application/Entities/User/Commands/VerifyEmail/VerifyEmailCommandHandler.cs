using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler(IYkotikaDbContext dbContext) : IRequestHandler<VerifyEmailCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(UserModel), request.UserId);
            }

            if (user is DefaultUserModel)
            {
                return;
            }

            var defaultUser = new DefaultUserModel
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
            };

            _dbContext.Users.Remove(user);
            _dbContext.Users.Add(defaultUser);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
