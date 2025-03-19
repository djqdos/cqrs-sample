using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Website.ConsumptionDays;
using Website.Models;

namespace Website.Hubs
{
    public class ConsumptionStreamHub : Hub
    {
        private readonly ConsumptionDaysDbContext _dbContext;
        public ConsumptionStreamHub(ConsumptionDaysDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// This is a 'streamable' version
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async IAsyncEnumerable<ConsumptionDay> GetStreamAsync([EnumeratorCancellation] CancellationToken token)
        {
            Random r = new Random();

            // This doesn't actually 'finalize' the data at this point, as denoted by the 'AsAsyncEnumerable()'
            // You would need to 'ToList()' or something to materialize the data
            //var items = _dbContext.ConsumptionDays.OrderByDescending(x => x.Date).AsNoTracking().AsAsyncEnumerable();
            var items = _dbContext.ConsumptionDays.AsAsyncEnumerable().WithCancellation(token);

            // Because this is an asyncronous collection, we can await on the foreach
            await foreach(var item in items)
            {

                // return each item, as it gets it - hence the 'streaming' aspect.
                // So, we don't need to wait until we've got every item in the collection before we can start to send the data.
                // Might be useful in situations where there's a _lot_ of data to return
                // Also, should help memory usage, because we're not creating a definitive list in memory, before we actually do anything
                // with it.
                yield return item;

                // added synthetic delay, to simulate a long-running, or complicated process that could take a while to process.
                await Task.Delay(r.Next(50, 100));
            }
        }


        /// <summary>
        /// This is the 'normal' way of doing things
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<ConsumptionDay>> GetStreamAsync2(CancellationToken token)
        {
            var items = await _dbContext.ConsumptionDays.OrderByDescending(x => x.Date).AsNoTracking().ToListAsync();

            return items;
        }
    }
}
