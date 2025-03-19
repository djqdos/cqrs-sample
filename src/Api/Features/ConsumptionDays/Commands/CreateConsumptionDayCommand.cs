using Api.Features.ConsumptionDays.Models;
using SharedMessages;

namespace Api.Features.ConsumptionDays.Commands
{
    public class CreateConsumptionDayCommand
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public DayComplete DayComplete { get; set; }

        public Guid MeterId { get; set; }
    }
}
