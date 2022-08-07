namespace MovieClub.Infrastructure.External.TMDb.Models;
public class TMDbCredit
{
    public TMDbCast[]? Cast { get; set; }
    public TMDbCrew[]? Crew { get; set; }
}

public class TMDbCast
{
    public string? Name { get; set; }

}

public class TMDbCrew
{
    public string? Name { get; set; }

    public string? Job { get; set; }


}
