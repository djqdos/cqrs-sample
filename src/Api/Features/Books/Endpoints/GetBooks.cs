namespace Api.Features.Books.Endpoints
{
	public class GetBooks :IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app)
		{
			app.MapGet("book", async () =>
			{
				await Task.Delay(0);
				return Results.Ok("fdghdjgk");
			});
		}
	}
}
