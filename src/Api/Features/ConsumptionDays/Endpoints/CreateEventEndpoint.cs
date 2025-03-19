
using SharedMessages;
using Wolverine;

namespace Api.Features.ConsumptionDays.Endpoints
{
    public class CreateEventEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("create", async (IMessageBus messageBus) =>
            {
                await Task.Delay(0);

                ConsumptionDayCreatedEvent newEvent = new ConsumptionDayCreatedEvent
                {
                    Complete = DayComplete.Complete,
                    Date = DateTime.Now,
                    Id = Guid.NewGuid(),
                    MeterId = Guid.NewGuid()
                };

                await messageBus.PublishAsync(newEvent);
            });
        }
    }
}
