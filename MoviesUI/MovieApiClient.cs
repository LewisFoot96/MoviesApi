using Movies.Shared.Dtos;

namespace MoviesUI;

public class MovieApiClient (HttpClient httpClient)
{
    public async Task<MovieDto[]> GetMoviesAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<MovieDto>? movies = null;

        await foreach (var movie in httpClient.GetFromJsonAsAsyncEnumerable<MovieDto>("/api/movies?movieTitle=The&pageCount=10&currentPageNumber=1", cancellationToken))
        {
            if (movies?.Count >= maxItems)
            {
                break;
            }

            if (movie is not null)
            {
                movies ??= [];
                movies.Add(movie);
            }
        }

        return movies?.ToArray() ?? [];
    }
}
