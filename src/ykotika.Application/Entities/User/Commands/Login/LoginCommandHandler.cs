using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Commands.Login
{
    public class LoginCommandHandler(
        IYkotikaDbContext dbContext, 
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher) 
        : IRequestHandler<LoginCommand, string>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email ==  request.Email, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(UserModel), request.Email);
            }

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Incorrect password");
            }
            var token = _jwtProvider.Generate(user);

            return token;
        }
    }
}
