namespace MovieClub.Web.Common.DTOs;
public class MeetupDTO
{
    public int Id { get; set; }

    public int ClubId { get; set; }
    public ClubDTO? Club { get; set; }

    public int MovieId { get; set; }
    public MovieDTO? Movie { get; set; }

    public bool MovieHidden { get; set; }

    public string? Host { get; set; }

    public DateTime Date { get; set; }
}
