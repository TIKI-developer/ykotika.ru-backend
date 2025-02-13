using Ykotika.Domain.Entities;

namespace Ykotika.Application.Models
{
    public class ProductFilterDto
    {
        public bool? IsPublished { get; set; }
        public ProductStatus? Status { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductTypeId { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
