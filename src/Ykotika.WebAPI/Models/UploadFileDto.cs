namespace Ykotika.WebAPI.Models
{
    public class UploadFileDto
    {
        public required IFormFile File { get; set; }
        public string? RelativePath { get; set; }
    }
}
