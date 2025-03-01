using MediatR;
using MoviesApi.Application.Commands.Handlers;

namespace MoviesApi.Endpoints;

public static class MoviesEndpoints
{
    public static void MapMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/movies");
        //Minimal apis use method injection
        group.MapPost("", CreateWeather);
    }

    public static async Task<IResult> CreateWeather(string movieName, IMediator sender)
    {
        var result = await sender.Send(new CreateMovieCommand(movieName));

        return TypedResults.NoContent();
    }
}