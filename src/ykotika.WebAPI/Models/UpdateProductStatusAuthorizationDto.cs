using Ykotika.Application.Common.Mappings;
using Ykotika.Domain.Entities;

namespace Ykotika.WebAPI.Models
{
    public class UpdateProductStatusAuthorizationDto
    {
        public required ProductStatus From { get; set; }
        public required ProductStatus To { get; set; }
    }
}
