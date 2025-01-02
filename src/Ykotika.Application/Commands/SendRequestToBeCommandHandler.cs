using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class SendRequestToBeCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<SendRequestToBeCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(SendRequestToBeCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.UserId);

            if (user.Permissions.Contains(UserPermission.Author))
            {
                throw new Exception("User is author already or send request");
            }

            user.Permissions.Add(UserPermission.Author);
            user.Name = request.Name;
            user.Surname = request.Surname;
            user.PhoneNumber = request.PhoneNumber;
            user.Timestamps.MarkUpdated();

            var author = new Author
            {
                Id = Guid.NewGuid(),
                Socials = request.Socials,
                User = user,
                Request = new Domain.ValueObjects.AuthorRequest
                {
                    TellAboutYourself = request.TellAboutYourself,
                    WhichSocial = request.WhichSocial,
                    Timestamps = new Domain.ValueObjects.Timestamps()
                },
                Status = AuthorStatus.New,
                Timestamps = new Domain.ValueObjects.Timestamps()
            };

            await _dbContext.Authors.AddAsync(author, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return author.Id;
        }
    }
}
