using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Entities.User.Commands.Login
{
    public class LoginCommandHandler(
        IYkotikaDbContext dbContext,
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher)
        : IRequestHandler<LoginCommand, LoginViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<LoginViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("User doesn't exist, or invalid password");
            }

            if (!user.IsEmailVerified)
            {
                throw new Exception("Email not verified");
            }

            var token = _jwtProvider.GenerateAccessToken(user);

            return new LoginViewModel { AccessToken = token };
        }
    }
}
