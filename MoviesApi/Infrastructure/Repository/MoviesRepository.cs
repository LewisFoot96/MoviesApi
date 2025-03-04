using Dapper;
using Microsoft.Data.SqlClient;
using MoviesApi.Application.Repository;
using MoviesApi.Domain;

namespace MoviesApi.Infrastructure.Repository;

public class MoviesRepository(SqlConnection client) : IRepository<Movie>
{
    private SqlConnection Client { get; } = client;

    private const string _sqlStatement =
            """
            SELECT * FROM Movie 
            WHERE Title LIKE @name
            
            ORDER BY Title

            OFFSET @pageNumber ROWS 
            FETCH NEXT @pageCount ROWS ONLY
            """;

    public async Task<List<Movie>> GetMoviesAsync(string name, int pageCount, int pageNumber, CancellationToken cancellation = default)
    {
        var parameters = new { name = "%" + name + "%", pageCount, pageNumber };

        IEnumerable<Movie> movies = [];

        await Client.OpenAsync();
        movies = await Client.QueryAsync<Movie>(_sqlStatement, parameters);
        await Client.CloseAsync();

        return movies.ToList();
    }
}