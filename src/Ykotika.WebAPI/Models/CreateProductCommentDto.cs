namespace Ykotika.WebAPI.Models
{
    public class CreateProductCommentDto
    {
        public required Guid Id { get; set; }
        public required string Content { get; set; }
    }
}
