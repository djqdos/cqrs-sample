namespace Api.Features.Books.Endpoints
{
	public class GetBooks :IEndpoint
	{
		public void MapEndpoint(IEndpointRouteBuilder app)
		{
			app.MapGet("book", async () =>
			{
				return Results.Ok("fdghdjgk");
			});
		}
	}
}
