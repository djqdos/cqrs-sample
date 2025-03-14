
using Api.Features.Books.Commands;
using System.Threading.Channels;

namespace Api.Features.Books.Endpoints
{
	public class AddBookEndpoint : IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app)
		{
			app.MapPost("book", async (Channel<TestCommand> channel) =>
			{

				await channel.Writer.WriteAsync(new TestCommand { Id = Guid.NewGuid(), Name = "Test Name" });



				return Results.Ok();
			});
		}
	}
}
