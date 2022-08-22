namespace MovieClub.Domain;
public class Movie : BaseEntity
{
    public Movie(int tmdbId)
    {
        TMDbId = tmdbId;
    }

    public int TMDbId { get; set; }

    public string? Cast { get; set; }
    public string? Director { get; set; }
    public string? Genres { get; set; }
    public string? IMDbPath { get; set; }
    public string? LetterboxPath { get; set; }
    public string? PosterURL { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int? Runtime { get; set; }
    public string? Screenwriter { get; set; }
    public string? Synopsis { get; set; }
    public string? Tagline { get; set; }
    public string? Title { get; set; }

    private Movie() { }
}
