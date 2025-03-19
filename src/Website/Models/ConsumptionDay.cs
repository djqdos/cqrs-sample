using SharedMessages;

namespace Website.Models
{
    public class ConsumptionDay
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public DayComplete DayComplete { get; set; }

        public Guid MeterId { get; set; }
    }
}
