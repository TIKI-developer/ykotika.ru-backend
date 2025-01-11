namespace Ykotika.Domain.Entities
{
    public class OutsourceShop : Entity
    {
        public required string Name { get; set; }
        public required string Link { get; set; }
        public required File Image { get; set; }
    }
}
