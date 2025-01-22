using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class VerifyEmailCommandHandler
        (IYkotikaDbContext dbContext,
        IRefreshTokenHasher refreshTokenHasher,
        IJwtProvider jwtProvider)
        :
        IRequestHandler<VerifyEmailCommand, LoginResponse>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IRefreshTokenHasher _refreshTokenHasher = refreshTokenHasher;

        public async Task<LoginResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            if (!user.Roles.Contains(UserRole.Unverified))
            {
                throw new Exception(Messages.ALREADY_VERIFIED);
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                User = user,
                Timestamps = new Domain.ValueObjects.Timestamps()
            };

            user.Roles.Remove(UserRole.Unverified);
            user.Roles.Add(UserRole.Verified);
            user.Timestamps.MarkUpdated();

            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            string accessToken = _jwtProvider.GenerateAccessToken(user, request.Issuer, request.Audience);
            string refreshToken = _jwtProvider.GenerateRefreshToken();
            user.RefreshTokenHash = _refreshTokenHasher.Encode(refreshToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }

}
