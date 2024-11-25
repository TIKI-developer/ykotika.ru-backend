using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Author.Commands
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
                .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException(nameof(UserModel), request.UserId);
            }
            if (!request.ConfirmedOffer)
            {
                throw new Exception("Confirm offer!");
            }

            if (user.Roles.Contains(UserRole.Author))
            {
                throw new Exception("User is author already or send request");
            }

            user.Roles.Add(UserRole.Author);
            user.MarkUpdated();

            var author = new AuthorModel
            {
                UserId = request.UserId,
                Surname = request.Surname,
                PhoneNumber = request.PhoneNumber,
                Socials = request.Socials,
                Request = new AuthorRequest
                {
                    TellAboutYourself = request.TellAboutYourself,
                    WhichSocial = request.WhichSocial,
                },
                Status = AuthorStatus.New,
                ConfirmedOffer = request.ConfirmedOffer,
            };

            await _dbContext.Authors.AddAsync(author, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
