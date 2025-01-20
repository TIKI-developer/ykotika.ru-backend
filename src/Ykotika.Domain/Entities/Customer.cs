namespace Ykotika.Domain.Entities
{
    public class Customer : Entity
    {
        public required User User { get; set; }
    }
}
