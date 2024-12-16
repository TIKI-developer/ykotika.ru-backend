using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Entities.User.Commands.Login;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler
        (IYkotikaDbContext dbContext,
        IJwtProvider jwtProvider)
        :
        IRequestHandler<VerifyEmailCommand, LoginViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<LoginViewModel> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
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
                throw new Exception("User already verified");
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

            var token = _jwtProvider.GenerateAccessToken(user);

            return new LoginViewModel { AccessToken = token };
        }
    }

}
