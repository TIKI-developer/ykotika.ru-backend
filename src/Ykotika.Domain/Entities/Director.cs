namespace Ykotika.Domain.Entities
{
    public class Director : Entity 
    {
        public required User User { get; set; }
    }
}
