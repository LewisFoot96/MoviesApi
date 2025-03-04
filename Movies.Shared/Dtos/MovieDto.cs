namespace Movies.Shared.Dtos
{
    public record MovieDto(
    string Title,
    DateTime Release_Date,
    float Popularity,
    float Vote_Count,
    float Vote_Average,
    string Original_Language,
    string Genre,
    string Poster_Url);
}
