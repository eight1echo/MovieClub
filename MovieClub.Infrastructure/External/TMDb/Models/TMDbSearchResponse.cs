namespace MovieClub.Infrastructure.External.TMDb.Models;
public class TMDbSearchResponse
{
    public int Page { get; set; }
    public List<TMDbSearchResult>? Results { get; set; }
    public int Total_Pages { get; set; }
    public int Total_Results { get; set; }
}
