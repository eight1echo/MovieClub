namespace MovieClub.Web.Areas.Clubs.Pages.Meetups;

public class ClubMeetupsModel
{
    public int ClubId { get; set; }
    public string? ClubName { get; set; }
    public List<MeetupDTO> Meetups { get; set; } = new List<MeetupDTO>();
}
