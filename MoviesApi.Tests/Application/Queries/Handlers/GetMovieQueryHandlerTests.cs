using MoviesApi.Application.Queries;
using MoviesApi.Application.Queries.Handlers;
using MoviesApi.Application.Repository;
using MoviesApi.Application.Responses;
using MoviesApi.Domain;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace MoviesApi.Tests.Application.Queries.Handlers;

//TODO - add stryker mutation testing to test tests
public class GetMovieQueryHandlerTests
{
    private readonly IRepository<Movie> _movieRepositoryMock;

    public GetMovieQueryHandlerTests()
    {
        _movieRepositoryMock = Substitute.For<IRepository<Movie>>();
    }

    [Fact]
    public async Task GetMovieQueryHandler_MoviesFound_ReturnsMovies()
    {
        // Arrange
        var movie = new Movie
        {
            Id = Guid.NewGuid(),
            Genre = "Test",
            Original_Language = "en",
            Popularity = 1,
            Poster_Url = "https://test.com",
            Release_Date = DateTime.UtcNow,
            Title = "Test",
            Vote_Average = 1,
            Vote_Count = 1
        };

        var query = new GetMovieQuery("Test", 1, 1);

        var queryHandler = new GetMovieQueryHandler(_movieRepositoryMock);

        _movieRepositoryMock.GetMoviesAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns([movie]);
        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MovieResponse>(result);
        Assert.Single(result.AsT0.Movies);
    }

    [Fact]
    public async Task GetMovieQueryHandler_NoMoviesReturned_NotFoundReturned()
    {
        // Arrange
        var query = new GetMovieQuery("Test", 1, 1);
        var queryHandler = new GetMovieQueryHandler(_movieRepositoryMock);
        _movieRepositoryMock.GetMoviesAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).Returns([]);

        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MovieResponse>(result);
        Assert.NotNull(result.AsT1);
    }

    [Fact]
    public async Task GetMovieQueryHandler_ErrorWhenGettingMovies_ErrorReturned()
    {
        // Arrange
        var errorMessage = "error";
        var query = new GetMovieQuery("Test", 1, 1);
        var queryHandler = new GetMovieQueryHandler(_movieRepositoryMock);
        _movieRepositoryMock.GetMoviesAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>()).Throws(new Exception(errorMessage));

        // Act
        var result = await queryHandler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<MovieResponse>(result);
        Assert.NotNull(result.AsT2);
        Assert.Equal($"Unexpected error when creating location. Exception: {errorMessage}", result.AsT2.ExceptionMessage);
    }
}
