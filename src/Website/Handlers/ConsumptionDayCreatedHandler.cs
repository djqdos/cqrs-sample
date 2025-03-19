using Microsoft.AspNetCore.SignalR;
using SharedMessages;
using Website.ConsumptionDays;
using Website.Hubs;
using Website.Models;
using Wolverine.Attributes;

namespace Website.Handlers
{
    [WolverineHandler]
    public class ConsumptionDayCreatedHandler
    {
        public async Task HandleAsync(ConsumptionDayCreatedEvent createdEvent, ConsumptionDaysDbContext db, ConsumptionStreamHub hub)
        {
            await Task.Delay(0);

            var consumptionDay = new ConsumptionDay
            {
                Date = createdEvent.Date,
                DayComplete = createdEvent.Complete,
                Id = createdEvent.Id,
                MeterId = createdEvent.MeterId
            };

            await db.ConsumptionDays.AddAsync(consumptionDay);

            await db.SaveChangesAsync();

        }
    }
}
