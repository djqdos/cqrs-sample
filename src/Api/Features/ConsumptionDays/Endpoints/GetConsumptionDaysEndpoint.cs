
using Microsoft.EntityFrameworkCore;

namespace Api.Features.ConsumptionDays.Endpoints
{
    public class GetConsumptionDaysEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("consumption", async (ConsumptionDaysDbContext dbContext) =>
            {

                var results = await dbContext.ConsumptionDays.ToListAsync(CancellationToken.None);


                return Results.Ok(results);
            });
        }
    }
}
