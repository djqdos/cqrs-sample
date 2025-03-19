using Api.Features.Books.Commands;
using Api.Features.ConsumptionDays.Models;
using Microsoft.EntityFrameworkCore;
using SharedMessages;
using System.Threading.Channels;

namespace Api.Features.ConsumptionDays.Endpoints
{
    public class AddConsumptionDayEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("addconsumption", async (ConsumptionDaysDbContext dbContext) =>
            {

                var count = 100;
                var meterId = Guid.NewGuid();
                Random random = new Random();

                List<ConsumptionDay> _tmp = new List<ConsumptionDay>();

                DateTime dateOnly = DateTime.Now.AddDays(-count);
                for (int i = 0; i <= count; i++)
                {
                    var enumVal = Enum.GetValues<DayComplete>();
                    var day = new ConsumptionDay
                    {
                        Id = Guid.NewGuid(),
                        Date = dateOnly.AddDays(i),
                        DayComplete = enumVal[random.Next(enumVal.Length)],
                        //DayComplete = DayComplete.Complete,
                        MeterId = meterId
                    };

                    _tmp.Add(day);
                }


                // randomise the order - for shits n giggles
                _tmp = _tmp.OrderBy(_ => Guid.NewGuid()).ToList();


                await dbContext.ConsumptionDays.AddRangeAsync(_tmp);
                await dbContext.SaveChangesAsync();

                return Results.Ok();
            });
        }
    }
}
