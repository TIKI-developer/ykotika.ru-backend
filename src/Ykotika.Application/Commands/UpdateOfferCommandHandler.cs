using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

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
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Offer), request.Id);

            offer.Content = request.Content ?? offer.Content;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
