namespace Ykotika.Domain.ValueObjects
{
    public class ImageListItem
    {
        public required int OrderIndex { get; set; }
        public required Entities.File Image { get; set; }
    }
}
