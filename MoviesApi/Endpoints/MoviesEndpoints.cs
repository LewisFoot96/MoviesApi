using MediatR;
using MoviesApi.Application.Queries;

namespace MoviesApi.Endpoints;

public static class MoviesEndpoints
{
    public static void MapMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/movies");
        //Minimal apis use method injection
        group.MapGet("", GetMovie);
    }

    public static async Task<IResult> GetMovie(string movieTitle, int pageCount, int currentPageNumber, IMediator sender)
    {
        var movies = await sender.Send(new GetMovieQuery(movieTitle, pageCount, currentPageNumber));

        return movies.Match<IResult>(
            movies => TypedResults.Ok(movies.Movies),
            _ => TypedResults.NotFound(),
            error => TypedResults.InternalServerError(error));
    }
}