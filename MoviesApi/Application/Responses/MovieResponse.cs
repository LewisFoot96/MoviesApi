using MoviesApi.Application.DTOs;
using OneOf;

namespace MoviesApi.Application.Responses;

[GenerateOneOf]
public partial class MovieResponse : OneOfBase<MovieResponse.MoviesResponse,
    MovieResponse.NotFound,
    MovieResponse.Error>
{
    public record MoviesResponse(IReadOnlyCollection<MovieDto> Movies);
    
    public record NotFound;

    public record Error(string ExceptionMessage);
}
