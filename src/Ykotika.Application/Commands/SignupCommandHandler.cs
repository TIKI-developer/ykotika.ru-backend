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
        : IRequestHandler<SignupCommand, Signup>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<Signup> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var existUser = await
                _dbContext
                .Users
                .Include(e => e.Timestamps)
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            bool userExist = existUser != null;
            string token;

            if (!userExist)
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                    Timestamps = new Timestamps(),
                    PasswordHash = _passwordHasher.Generate(request.Password),
                    ConfirmedPolicy = true
                };

                await _dbContext.Users.AddAsync(user, cancellationToken);

                token = _jwtProvider.GenerateAccessToken(user);
            }
            else
            {
                bool userIsGuest = existUser!.Permissions.Contains(UserPermission.Unverified);

                if (userIsGuest)
                {
                    existUser.Name = request.Name;
                    existUser.Email = request.Email;
                    existUser.PasswordHash = _passwordHasher.Generate(request.Password);
                    existUser.Timestamps.MarkUpdated();
                    token = _jwtProvider.GenerateAccessToken(existUser);
                }
                else
                {
                    throw new UserAlreadyRegistered(request.Email);
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Signup { AccessToken = token };
        }
    }
}
