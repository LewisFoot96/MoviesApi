using Movies.Shared.Dtos;
using MoviesApi.Domain;

namespace MoviesApi.Application.Mappers;

public static class MovieMapper
{
    public static MovieDto MapMovieToMovieDto(this Movie movie)
    {
        return new MovieDto(movie.Title, movie.Release_Date, movie.Popularity, movie.Vote_Count, movie.Vote_Average, movie.Original_Language, movie.Genre, movie.Poster_Url);
    }
}
