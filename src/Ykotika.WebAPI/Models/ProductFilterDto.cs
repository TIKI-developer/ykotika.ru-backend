namespace Ykotika.WebAPI.Models
{
    public class ProductFilterDto
    {
        public bool? IsPublished { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ProductType { get; set; }
    }
}
