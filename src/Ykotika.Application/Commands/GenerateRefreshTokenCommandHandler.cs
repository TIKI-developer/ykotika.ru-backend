using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class GenerateRefreshTokenCommandHandler
        (IYkotikaDbContext dbContext,
        IRefreshTokenHasher refreshTokenHasher,
        IJwtProvider jwtProvider)
        : IRequestHandler<GenerateRefreshTokenCommand, LoginResponse>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IRefreshTokenHasher _refreshTokenHasher = refreshTokenHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<LoginResponse> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken)
                ?? throw new Exception("You need to authenticate!");

            if (user.RefreshTokenHash == null)
            {
                throw new Exception("You need to authenticate!");
            }

            bool isValid = _refreshTokenHasher.Verify(request.RefreshToken, user.RefreshTokenHash);

            if (!isValid)
            {
                throw new Exception("Token is not valid!");
            }

            var refreshToken = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokenHash = _refreshTokenHasher.Encode(refreshToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new LoginResponse
            {
                AccessToken = _jwtProvider.GenerateAccessToken(user),
                RefreshToken = refreshToken
            };
        }
    }
}
