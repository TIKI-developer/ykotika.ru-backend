namespace Ykotika.WebAPI.Models
{
    public class UpdateProductStatusMultipleDto
    {
        public required List<Guid> ProductIds { get; set; }
        public required string NewStatus { get; set; }
    }
}
