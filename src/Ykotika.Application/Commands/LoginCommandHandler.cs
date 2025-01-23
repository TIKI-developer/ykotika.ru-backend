using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;

namespace Ykotika.Application.Commands
{
    public class LoginCommandHandler(
        IYkotikaDbContext dbContext,
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher,
        IRefreshTokenHasher refreshTokenHasher)
        : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IRefreshTokenHasher _refreshTokenHasher = refreshTokenHasher;

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

            if (user == null || !_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception(Messages.LOGIN_ERROR);
            }

            if (!user.IsEmailVerified)
            {
                throw new Exception(Messages.EMAIL_NOT_VERIFIED);
            }

            var accessToken = _jwtProvider.GenerateAccessToken(user, request.Issuer, request.Audience);
            var refreshToken = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokenHash = _refreshTokenHasher.Encode(refreshToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
