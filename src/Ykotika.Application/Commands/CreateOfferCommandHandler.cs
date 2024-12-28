using MediatR;
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

        public async Task<Guid> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = new Offer
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                Timestamps = new Timestamps()
            };


            await _dbContext.Offers.AddAsync(offer, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return offer.Id;
        }
    }
}
