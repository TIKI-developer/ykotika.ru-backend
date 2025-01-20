using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class SignupCommandHandler(
        IYkotikaDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
        : IRequestHandler<SignupCommand, SignupResponse>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<SignupResponse> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var existUser = await
                _dbContext
                .Users
                .Include(e => e.Timestamps)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            bool userExist = existUser != null;
            string accessToken;

            if (!request.ConfirmedPersonalDataProcessingPolicy)
            {
                throw new Exception("You need confirm personal data processing policy!");
            }

            if (!userExist)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                    Timestamps = new Timestamps(),
                    PasswordHash = _passwordHasher.Generate(request.Password),
                    ConfirmedPersonalDataProcessingPolicy = request.ConfirmedPersonalDataProcessingPolicy
                };

                await _dbContext.Users.AddAsync(user, cancellationToken);

                accessToken = _jwtProvider.GenerateAccessToken(user);
            }
            else
            {
                bool userIsGuest = existUser!.Roles.Contains(UserRole.Unverified);

                if (userIsGuest)
                {
                    existUser.Name = request.Name;
                    existUser.Email = request.Email;
                    existUser.PasswordHash = _passwordHasher.Generate(request.Password);
                    existUser.Timestamps.MarkUpdated();
                    accessToken = _jwtProvider.GenerateAccessToken(existUser);
                }
                else
                {
                    throw new UserAlreadyRegistered(request.Email);
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SignupResponse { AccessToken = accessToken };
        }
    }
}
