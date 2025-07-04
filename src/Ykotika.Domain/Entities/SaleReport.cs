using Ykotika.Domain.ValueObjects;

namespace Ykotika.Domain.Entities
{
    public class SaleReport : Entity
    {
        public required PayoutStatus PayoutStatus { get; set; }
        public required DateTime PaidAt { get; set; }
        public required DateTimeInterval Period { get; set; }
        public required User Author { get; set; }
        public required List<Sale> Sales { get; set; }
        public required List<File> Attachments { get; set; }
    }

    public enum PayoutStatus
    {
        Unpaid,
        Paid
    }
}
