using Ykotika.Application.Commands;
using Ykotika.Application.Common.Mappings;

namespace Ykotika.WebAPI.Models
{
    public class CreateCategoryDto : IMapWith<CreateCategoryCommand>
    {
        public required Guid FormId { get; set; }
    }
}
