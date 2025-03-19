using Api.Features.ConsumptionDays.Commands;
using Api.Features.ConsumptionDays.Models;

namespace Api.Features.ConsumptionDays.Handlers
{
    public class CreateConsumptionDayHandler
    {
        private readonly ConsumptionDaysDbContext _dbContext;

        public CreateConsumptionDayHandler(ConsumptionDaysDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateConsumptionDayCommand createConsumptionDayCommand)
        {

            var consumptionDay = new ConsumptionDay
            {
                Id = Guid.NewGuid(),
                Date = createConsumptionDayCommand.Date,
                DayComplete = createConsumptionDayCommand.DayComplete,
                MeterId = createConsumptionDayCommand.MeterId
            };


            await _dbContext.ConsumptionDays.AddAsync(consumptionDay);


            // apparently, we don't need to call savechanges, as wolverine sees that the handler has a dependency
            // on 
            //await _dbContext.SaveChangesAsync(); 


        }
    }
}
