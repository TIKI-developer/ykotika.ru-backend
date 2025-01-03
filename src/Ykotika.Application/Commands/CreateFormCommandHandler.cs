using MediatR;
using NanoidDotNet;
using Ykotika.Application.Interfaces;
using Ykotika.Domain.Entities;
using Ykotika.Domain.ValueObjects;

namespace Ykotika.Application.Commands
{
    public class CreateFormCommandHandler
        (IYkotikaDbContext dbContext)
        :
        IRequestHandler<CreateFormCommand, Guid>
    {
        private readonly IYkotikaDbContext _dbContext = dbContext;

        public async Task<Guid>
            Handle(CreateFormCommand request, CancellationToken cancellationToken)
        {
            var inputs = new List<Form.Input>();

            foreach (var (dto, index) in request.Inputs.Select((dto, index) => (dto, index)))
            {
                var input = new Form.Input
                {
                    Id = Nanoid.Generate(size: 6),
                    OrderIndex = index,
                    Type = dto.Type,
                    ExtraAttributes = new Form.InputExtraAttributes
                    {
                        Label = dto.ExtraAttributes.Label,
                        Placeholder = dto.ExtraAttributes.Placeholder,
                        IsRequired = dto.ExtraAttributes.IsRequired
                    }
                };
                inputs.Add(input);
            }

            var form = new Form
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Inputs = inputs,
                Timestamps = new Timestamps(),
                IsPublished = request.IsPublished
            };

            await _dbContext.Forms.AddAsync(form, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return form.Id;
        }
    }
}
