using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Application.ViewModels;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands.User
{
    public class VerifyEmailCommandHandler
        (IYkotikaDbContext dbContext,
        IJwtProvider jwtProvider)
        :
        IRequestHandler<VerifyEmailCommand, LoginResponse>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<LoginResponse> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            if (!user.Permissions.Contains(UserPermission.Unverified))
            {
                throw new Exception(Messages.ALREADY_VERIFIED);
            }

            var customer = new Customer
            {
                Id = user.Id,
            };

            user.Permissions.Remove(UserPermission.Unverified);
            user.Permissions.Add(UserPermission.Customer);
            user.Timestamps.MarkUpdated();

            _dbContext.Customers.Add(customer);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var token = _jwtProvider.GenerateAccessToken(user);

            return new LoginResponse { AccessToken = token };
        }
    }

}
