namespace Ykotika.Domain.ValueObjects
{
    public class Timestamps
    {
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
        public void MarkUpdated()
        {
            UpdatedAt = DateTime.UtcNow;
        }
        public Timestamps()
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
