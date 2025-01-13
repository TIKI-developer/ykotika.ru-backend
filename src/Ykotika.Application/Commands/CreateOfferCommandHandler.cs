using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateOfferCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateOfferCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid>
            Handle(CreateOfferCommand request,
                   CancellationToken cancellationToken)
        {
            var author = await
                _dbContext
                .Users
                .FirstOrDefaultAsync(e => e.Id == request.AuthorId, cancellationToken)
                ?? throw new NotFoundException(nameof(User), request.AuthorId);

            var offer = new Offer
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                Timestamps = new Timestamps(),
                IsPublished = false,
                User = author
            };

            await _dbContext.Offers.AddAsync(offer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return offer.Id;
        }
    }
}
