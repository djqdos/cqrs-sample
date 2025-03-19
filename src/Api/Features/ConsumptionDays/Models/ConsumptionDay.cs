using SharedMessages;

namespace Api.Features.ConsumptionDays.Models
{
    public class ConsumptionDay
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public DayComplete DayComplete { get; set; }

        public Guid MeterId { get; set; }
    }

}
