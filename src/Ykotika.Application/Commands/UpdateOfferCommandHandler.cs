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
        : IRequestHandler<UpdateOfferCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await
                _dbContext
                .Offers
                .Include(e => e.Timestamps)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Offer), request.Id);


            var newOffer = new Offer
            {
                Id = Guid.NewGuid(),
                Content = request.Content ?? offer.Content,
                Timestamps = new Timestamps()
            };
            newOffer.Timestamps.MarkUpdated();

            await _dbContext.Offers.AddAsync(newOffer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
