namespace Ykotika.Application.Models
{
    public class ProductFilterDto
    {
        public bool? IsPublished { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}
