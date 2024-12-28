using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class ChangePasswordCommandHandler 
        (IYkotikaDbContext dbContext,
        IPasswordHasher passwordHasher)
        : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            if (_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
            {
                user.PasswordHash = _passwordHasher.Generate(request.NewPassword);
            }
            else
            {
                throw new Exception("Старый пароль неправильный!");
            }

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
