namespace MovieClub.Domain;
public class Movie : BaseEntity
{
    public Movie(int tmdbId)
    {
        TMDbId = tmdbId;
    }

    public int TMDbId { get; set; }

    public string? IMDbId { get; set; }

    public string? Title { get; set; }

    public DateTime ReleaseDate { get; set; }

    public int? Runtime { get; set; }

    public string? Synopsis { get; set; }

    public string? Tagline { get; set; }

    public string? Genres { get; set; }

    public string? PosterURL { get; set; }

    public string? BackdropURL { get; set; }

    public string? Language { get; set; }

    public string? LanguageTitle { get; set; }

    public long? Budget { get; set; }
    public long? Revenue { get; set; }

    public virtual ICollection<Meetup> Meetups { get; set; } = new List<Meetup>();


    private Movie() { }
}
