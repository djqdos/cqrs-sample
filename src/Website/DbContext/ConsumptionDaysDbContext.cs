using Microsoft.EntityFrameworkCore;
using Website.Models;

namespace Website.ConsumptionDays
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
