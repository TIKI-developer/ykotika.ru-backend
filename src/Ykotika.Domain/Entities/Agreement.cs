namespace Ykotika.Domain.Entities
{
    public class Agreement : Entity
    {
        public required Offer Offer { get; set; }
        public required Author Author { get; set; }
    }
}
