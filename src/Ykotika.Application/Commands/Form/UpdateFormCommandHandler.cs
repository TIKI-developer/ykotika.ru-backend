using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;

namespace Ykotika.Application.Commands.Form
{
    public class UpdateFormCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<UpdateFormCommand>
    {
        private readonly IMapper _mapper = mapper;
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task Handle(UpdateFormCommand request, CancellationToken cancellationToken)
        {
            var form = await
                _dbContext
                .Forms
                .Include(f => f.Inputs)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(Form), request.Id);

            form.Name = request.Name ?? form.Name;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
