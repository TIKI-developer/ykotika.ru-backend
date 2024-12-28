using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands
{
    public class CreateAgreementCommandHandler
        (IYkotikaDbContext dbContext)
        : IRequestHandler<CreateAgreementCommand>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(CreateAgreementCommand request, CancellationToken cancellationToken)
        {
            var user = await
                _dbContext
                .Authors
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.User.Id == request.UserId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Author), request.UserId);

            var offer = await
                _dbContext
                .Offers
                .FirstOrDefaultAsync(e => e.Id == request.OfferId, cancellationToken)
                ?? throw new NotFoundException(nameof(Domain.Entities.Offer), request.OfferId);

            var agreement = new Domain.Entities.Agreement
            {
                Id = Guid.NewGuid(),
                Offer = offer,
                Author = user,
                Timestamps = new Domain.ValueObjects.Timestamps()
            };

            await _dbContext.Agreements.AddAsync(agreement, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
