using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateProductTypeDto : IMapWith<CreateProductTypeCommand>
    {
        public required Guid FormId { get; set; }
    }
}
