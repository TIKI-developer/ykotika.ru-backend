using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ykotika.Application.Common.Exceptions;
using Ykotika.Application.Interfaces;
using Ykotika.Domain;

namespace Ykotika.Application.Entities.Form.Commands.Update
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
                .Include(f => f.Fields)
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken)
                ?? throw new NotFoundException(nameof(FormModel), request.Id);

            form.Name = request.Name ?? form.Name;
            form.Fields = _mapper.Map<List<FormInputModel>>(request.Fields) ?? form.Fields;

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
