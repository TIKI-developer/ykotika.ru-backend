using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class ResetPasswordCommandHandler
        (IYkotikaDbContext dbContext,
        IJwtProvider jwtProvider,
        IPasswordHasher passwordHasher)
        : IRequestHandler<ResetPasswordCommand, LoginResponse>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<LoginResponse>
            Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var userEmail = _jwtProvider.VerifyPasswordRecoverToken(request.Token);
            if (string.IsNullOrEmpty(userEmail))
            {
                throw new Exception(Messages.TOKEN_NOT_VALID);
            }

            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Email == userEmail, cancellationToken)
                ?? throw new NotFoundException(nameof(User), userEmail);

            user.PasswordHash = _passwordHasher.Generate(request.NewPassword);
            var accessToken = _jwtProvider.GenerateAccessToken(user);
            var refreshToken = _jwtProvider.GenerateRefreshToken();

            await _dbContext.SaveChangesAsync(cancellationToken);
            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
