namespace Ykotika.Domain.Entities
{
    public class Moderator : Entity
    {
        public required User User { get; set; }
    }
}
