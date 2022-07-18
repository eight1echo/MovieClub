namespace MovieClub.Domain;
public class Movie : BaseEntity
{
    public int TMDbId { get; set; }

    public string? IMDbId { get; set; }

    public string? Title { get; set; }

    //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    //[DataType(DataType.Date)]
    public DateTime? ReleaseDate { get; set; }

    public int? Runtime { get; set; }

    public string? Synopsis { get; set; }

    public string? Tagline { get; set; }

    public string? PosterURL { get; set; }

    public string? BackdropURL { get; set; }

    public string? Language { get; set; }

    public string? LanguageTitle { get; set; }

    public long? Budget { get; set; }
    public long? Revenue { get; set; }

    //public virtual ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
    public virtual ICollection<Meetup> Meetups { get; set; } = new List<Meetup>();
}
