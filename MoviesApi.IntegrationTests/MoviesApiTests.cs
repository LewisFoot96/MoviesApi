using Movies.Shared.Dtos;
using System.Text.Json;

namespace MoviesApi.IntegrationTests;

public class MoviesApiTests
{
    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.MoviesApi_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();

        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("moviesapi");
        await resourceNotificationService.WaitForResourceAsync("moviesapi", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/api/movies?movieTitle=The&pageCount=10&currentPageNumber=1");

        response.EnsureSuccessStatusCode();
        var responseContent = await response.Content.ReadAsStreamAsync();
        var movies = await JsonSerializer.DeserializeAsync<List<MovieDto>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //TODO - requires database backup to be used will fail if not
        Assert.Equal(10, movies!.Count); //10 movies returned in accordance with paging
    }
}
