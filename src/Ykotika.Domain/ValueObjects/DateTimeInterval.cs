namespace Ykotika.Domain.ValueObjects
{
    public class DateTimeInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TimeSpan Duration => End - Start;
    }

}
