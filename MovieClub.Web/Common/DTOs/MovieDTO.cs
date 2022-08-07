namespace MovieClub.Web.Common.DTOs;
public class MovieDTO
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
    public int? Runtime { get; set; }

    public string? Director { get; set; }

    public string? Screenwriter { get; set; }

    public string? Cast { get; set; }

    public string? Synopsis { get; set; }

    public string? Tagline { get; set; }

    public string? Genres { get; set; }

    public string? PosterURL { get; set; }

    public string? BackdropURL { get; set; }

    public string? Language { get; set; }

    public string? LanguageTitle { get; set; }

    public long? Budget { get; set; }
    public long? Revenue { get; set; }
    public string? Title { get; set; }

}
