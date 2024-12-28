using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;

namespace Ykotika.Application.Commands
{
    public class UpdateProfileCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.Id);

            if (request.UserPictureFileId != null)
            {
                var userPicture = await
                    _dbContext
                    .Files
                    .FirstOrDefaultAsync(e => e.Id == request.UserPictureFileId, cancellationToken)
                    ?? throw new NotFoundException(nameof(Domain.Entities.File), request.UserPictureFileId);

                user.Picture = userPicture ?? user.Picture;
            }

            user.Name = request.Name ?? user.Name;
            user.Surname = request.Surname ?? user.Surname;
            user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
            

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
