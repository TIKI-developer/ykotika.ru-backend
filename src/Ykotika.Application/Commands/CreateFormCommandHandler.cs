using AutoMapper;
using MediatR;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;
namespace Ykotika.Application.Commands
{
    public class CreateFormCommandHandler
        (IYkotikaDbContext dbContext,
        IMapper mapper)
        :
        IRequestHandler<CreateFormCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var inputs = _mapper.Map<List<Input>>(request.Inputs);
            var form = new Form
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Inputs = inputs,
                Timestamps = new Timestamps()
            };
            foreach (var input in inputs)
            {
                input.Id = Guid.NewGuid();
                input.Form = form;
                await _dbContext.FormInputs.AddAsync(input, cancellationToken);
            }

            await _dbContext.Forms.AddAsync(form, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return form.Id;
        }
    }
}
