using MoviesApi.Domain;

namespace MoviesApi.Application.Repository;

public interface IRepository<T> where T : IAggregationRoot
{
    Task<List<T>> GetMoviesAsync(string name, int pageCount, int pageNumber, CancellationToken cancellationToken);
}