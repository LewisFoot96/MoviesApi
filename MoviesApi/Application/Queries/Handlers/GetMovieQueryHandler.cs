using MediatR;
using MoviesApi.Application.Mappers;
using MoviesApi.Application.Repository;
using MoviesApi.Application.Responses;
using MoviesApi.Domain;

namespace MoviesApi.Application.Queries.Handlers;

public class GetMovieQueryHandler(IRepository<Movie> repository) : IRequestHandler<GetMovieQuery, MovieResponse>
{
    //TODO add validation to query request 
    public async Task<MovieResponse> Handle(GetMovieQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var movies = await repository.GetMoviesAsync(request.MovieName, request.PageCount, request.PageNumber, cancellationToken);

            if(movies.Count == 0)
            {
                return new MovieResponse.NotFound();
            }

            return new MovieResponse.MoviesResponse(movies.Select(movie => movie.MapMovieToMovieDto()).ToList());
        }
        catch (Exception ex)
        {
            return new MovieResponse.Error($"Unexpected error when creating location. Exception: {ex.Message}");
        }
    }
}