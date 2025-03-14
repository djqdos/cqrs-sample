using Api.Features.Books.Commands;
using System.Threading.Channels;

namespace Api.Features.SomeOtherFeature
{
	public class SomeProcessor : BackgroundService
	{

		private readonly Channel<TestCommand> _channel;

		public SomeProcessor(Channel<TestCommand> channel)
		{
			_channel = channel;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while(await _channel.Reader.WaitToReadAsync(stoppingToken))
			{
				var request = await _channel.Reader.ReadAsync();

				//await Task.Delay(2000);
				Console.WriteLine($"Id = {request.Id}: Name = {request.Name}");
			}
		}
	}
}
