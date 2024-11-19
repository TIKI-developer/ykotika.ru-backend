using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.User.Commands.Signup
{
    public class SignupCommandHandler(
        IYkotikaDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider) 
        : IRequestHandler<SignupCommand, string>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<string> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            var client = new UserModel
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.Generate(request.Password),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            await _dbContext.Users.AddAsync(client, cancellationToken);

            var token = _jwtProvider.Generate(client);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return token;
        }
    }
}
