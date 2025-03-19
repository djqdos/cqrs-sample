using Api.Features.ConsumptionDays.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Features.ConsumptionDays
{
    public class ConsumptionDaysDbContext : DbContext
    {

        public ConsumptionDaysDbContext(DbContextOptions<ConsumptionDaysDbContext> options) 
            :base(options)
        {
                
        }


        public DbSet<ConsumptionDay> ConsumptionDays { get; set; }
    }
}
