namespace Ykotika.Domain.Entities
{
    public class Sale : Entity
    {
        public required float Royalty { get; set; }
        public required float Revenue { get; set; }
        public required Guid ProductId { get; set; }
        public required Product Product { get; set; }
    }
}
