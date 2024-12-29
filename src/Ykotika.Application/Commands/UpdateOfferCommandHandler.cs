using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class UpdateOfferCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<UpdateOfferCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await
                _dbContext
                .Offers
                .Include(e => e.Timestamps)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Offer), request.Id);

            if (!offer.IsPublished)
            {
                offer.Content = request.Content ?? offer.Content;
                offer.IsPublished = request.IsPublished ?? offer.IsPublished;
            }
            else
            {
                if (request.Content != null)
                {
                    if (request.Content.Equals(offer.Content))
                    {
                        return Guid.Empty;
                    }

                    var newOffer = new Offer
                    {
                        Id = Guid.NewGuid(),
                        Content = request.Content ?? offer.Content,
                        Timestamps = new Timestamps(),
                        IsPublished = true
                    };

                    await _dbContext.Offers.AddAsync(newOffer, cancellationToken);

                    return newOffer.Id;
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Guid.Empty;
        }
    }
}
