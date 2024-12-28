using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Offer
{
    public class DeleteOfferCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<DeleteOfferCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
        {
            var offer = await
                _dbContext
                .Offers
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Offer), request.Id);

            _dbContext.Offers.Remove(offer);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
