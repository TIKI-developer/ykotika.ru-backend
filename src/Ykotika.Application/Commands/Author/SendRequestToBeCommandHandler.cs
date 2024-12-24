using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands.Author
{
    public class SendRequestToBeCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<SendRequestToBeCommand, Unit>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Unit> Handle(SendRequestToBeCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            if (!request.ConfirmedOffer)
            {
                throw new Exception("Confirm offer!");
            }

            if (user.Permissions.Contains(UserPermission.Author))
            {
                throw new Exception("User is author already or send request");
            }

            user.Permissions.Add(UserPermission.Author);
            user.Timestamps.MarkUpdated();

            var author = new Domain.Entities.Author
            {
                Id = request.UserId,
                Socials = request.Socials,
                Request = new Domain.ValueObjects.AuthorRequest
                {
                    TellAboutYourself = request.TellAboutYourself,
                    WhichSocial = request.WhichSocial,
                    Timestamps = new Domain.ValueObjects.Timestamps()
                },
                Status = AuthorStatus.New
            };

            await _dbContext.Authors.AddAsync(author, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
