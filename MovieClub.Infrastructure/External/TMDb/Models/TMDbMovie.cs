namespace MovieClub.Infrastructure.External.TMDb.Models;
public class TMDbMovie
{
    public int Id { get; set; }
    public string? IMDb_Id { get; set; }

    public string? Title { get; set; }
    public DateTime? Release_Date { get; set; }
    public int? Runtime { get; set; }

    public string? Overview { get; set; }
    public string? Tagline { get; set; }
    public string? Poster_Path { get; set; }
    public string? Backdrop_Path { get; set; }
    public string? Original_Language { get; set; }
    public string? Original_Title { get; set; }

    public long? Budget { get; set; }
    public long? Revenue { get; set; }

    public TMDbCredit? Credits { get; set; }

    public ICollection<TMDbGenre>? Genres { get; set; }
}

  
