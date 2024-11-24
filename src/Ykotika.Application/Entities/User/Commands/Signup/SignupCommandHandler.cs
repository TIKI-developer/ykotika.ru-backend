using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Commands.Signup
{
    public class SignupCommandHandler(
        IYkotikaDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
        : IRequestHandler<SignupCommand, SignupViewModel>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<SignupViewModel> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var existUser = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            bool userExist = existUser != null;
            string token;

            if (!userExist)
            {
                var user = new UserModel
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Email = request.Email,
                    PasswordHash = _passwordHasher.Generate(request.Password),
                    ConfirmedPolicy = true
                };

                await _dbContext.Users.AddAsync(user, cancellationToken);

                token = _jwtProvider.GenerateAccessToken(user);
            }
            else
            {
                bool userIsGuest = existUser!.Roles.Contains(UserRole.Guest);

                if (userIsGuest)
                {
                    existUser.Name = request.Name;
                    existUser.Email = request.Email;
                    existUser.PasswordHash = _passwordHasher.Generate(request.Password);
                    existUser.MarkUpdated();
                    token = _jwtProvider.GenerateAccessToken(existUser);
                }
                else
                {
                    throw new UserAlreadyRegistered(request.Email);
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new SignupViewModel { AccessToken = token };
        }
    }
}
