namespace MoviesApi.Domain;

public class Movie : IAggregationRoot
{
    public Guid Id { get; init; }
    public required string Title { get; init; }
    public DateTime Release_Date { get; init; }
    public float Popularity { get; init; }
    public int Vote_Count { get; init; }
    public float Vote_Average { get; init; }
    public required string Original_Language { get; init; }
    public required string Genre { get; init; }
    public required string Poster_Url { get; init; }
}