using MediatR;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Offer
{
    public class CreateOfferCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateOfferCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid> Handle(CreateOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = new Domain.Entities.Offer
            {
                Id = Guid.NewGuid(),
                Content = request.Content,
                Timestamps = new Domain.ValueObjects.Timestamps()
            };


            await _dbContext.Offers.AddAsync(offer);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return offer.Id;
        }
    }
}
